using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class HelpRequestGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private HelpRequestGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HelpRequestGateway(DatabaseContext);
        }

        [Test]
        public void CreateHelpRequestReturnsTheRequestIfCreated()
        {
            var helpRequest = EntityHelpers.createHelpRequestEntity();
            var response = _classUnderTest.CreateHelpRequest(helpRequest);
            response.Should().Be(helpRequest.Id);
        }

        [Test]
        public void CreateDuplicateHelpRequestTheLatestAsMaster()
        {
            var helpRequest = _fixture.Create<HelpRequest>();
            helpRequest.Id = 0;
            var response1 = _classUnderTest.CreateHelpRequest(helpRequest.ToEntity());
            var response2 = _classUnderTest.CreateHelpRequest(helpRequest.ToEntity());
            var firstRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response1);
            var secondRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response2);
            firstRecordToCheck.RecordStatus.Should().Be("DUPLICATE");
            secondRecordToCheck.RecordStatus.Should().Be("MASTER");
        }

        [Test]
        public void PatchHelpRequestForHelpNeeded()
        {

            var helpRequest = _fixture.Build<HelpRequestEntity>()
                .Without(x => x.HelpRequestCalls)
                .With(x => x.HelpWithCompletingNssForm, true)
                .With(x => x.HelpWithShieldingGuidance, true)
                .With(x => x.HelpWithNoNeedsIdentified, true)
                .With(x => x.HelpWithAccessingSupermarketFood, false).Create();

            var dbEntity = DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();
            var patchRequestObject = new HelpRequestEntity()
            {
                HelpWithCompletingNssForm = false,
                HelpWithShieldingGuidance = false,
                HelpWithNoNeedsIdentified = null,
                HelpWithAccessingSupermarketFood = false
            };

            _classUnderTest.PatchHelpRequest(helpRequest.Id, patchRequestObject);

            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(helpRequest.Id);

            updatedEntity.HelpWithCompletingNssForm.Should().Be(false);
            updatedEntity.HelpWithShieldingGuidance.Should().Be(false);
            updatedEntity.HelpWithNoNeedsIdentified.Should().Be(true);
            updatedEntity.HelpWithAccessingSupermarketFood.Should().Be(false);

        }

        [Test]
        public void GetCallbacksReturnsCallbacks()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.RecordStatus = "MASTER";
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var hrParams = new CallbackRequestParams { HelpNeeded = null, Master = "True" };
            var response = _classUnderTest.GetCallbacks(hrParams);
            response.Should().BeEquivalentTo(helpRequests);
        }

        [Test]
        public void GetCallbacksWithWhitespaceInRequestParamStillReturnsCallbacks()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.RecordStatus = "MASTER ";
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var hrParams = new CallbackRequestParams { HelpNeeded = null, Master = "True" };
            var response = _classUnderTest.GetCallbacks(hrParams);
            response.Should().BeEquivalentTo(helpRequests);
        }

    }
}
