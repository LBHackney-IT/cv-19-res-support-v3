using System.Linq;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
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
            var helpRequest = _fixture.Build<HelpRequest>()
                .With(x => x.Uprn, "123")
                .With(x => x.DobMonth, "123")
                .With(x => x.DobDay, "123")
                .With(x => x.DobYear, "123")
                .With(x => x.ContactTelephoneNumber, "123")
                .With(x => x.ContactMobileNumber, "123")
                .Create();

            var helpRequest2 = _fixture.Build<HelpRequest>()
                .With(x => x.Uprn, "123")
                .With(x => x.DobMonth, "123")
                .With(x => x.DobDay, "123")
                .With(x => x.DobYear, "123")
                .With(x => x.ContactTelephoneNumber, "123")
                .With(x => x.ContactMobileNumber, "123")
                .Create();

            var response1 = _classUnderTest.CreateHelpRequest(helpRequest.ToEntity());
            var response2 = _classUnderTest.CreateHelpRequest(helpRequest2.ToEntity());
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
        public void GetHelpRequestReturnsEmptyCallsListIfNoCallsExist()
        {
            var id = 123;
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id));
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetHelpRequest(id);
            response.HelpRequestCalls.Should().BeNullOrEmpty();
        }

        [Test]
        public void GetHelpRequestReturnsCallsListIfCallsExist()
        {
            var id = 124;
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id));
            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = id);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetHelpRequest(id);
            response.HelpRequestCalls.Count.Should().Be(3);
            response.HelpRequestCalls.Should().BeEquivalentTo(calls);
        }

        [Test]
        public void GetAllCallbacksWithCallsListIfCallsExist()
        {
            var id = 124;
            var helpRequest = EntityHelpers.createHelpRequestEntity(id);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            helpRequest.CallbackRequired = true;

            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = id);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetCallbacks(new CallbackRequestParams() { HelpNeeded = "" });

            response.First().HelpRequestCalls.Count.Should().Be(3);
            response.First().HelpRequestCalls.Should().BeEquivalentTo(calls);
        }

        [Test]
        public void SearchRequestsReturnsCallsListIfCallsExist()
        {
            var id = 124;
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id));
            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = id);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.SearchHelpRequests(new RequestQueryParams());
            response.First().HelpRequestCalls.Count.Should().Be(3);
            response.First().HelpRequestCalls.Should().BeEquivalentTo(calls);
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
