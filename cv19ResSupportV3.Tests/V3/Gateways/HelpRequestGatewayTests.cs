using System;
using System.Linq;
using AutoFixture;
//using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class HelpRequestGatewayTests : DatabaseTests
    {
//        private readonly Fixture _fixture = new Fixture();
        private HelpRequestGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HelpRequestGateway(DatabaseContext);
        }

        [Test]
        public void CreateHelpRequestReturnsTheRequestIdIfCreated()
        {
            var helpRequestCommand = new CreateHelpRequest()
            {
                IsOnBehalf = true,
                ConsentToCompleteOnBehalf = true,
                OnBehalfFirstName = "Tim",
                CallbackRequired = true
            };
            var resident = new ResidentEntity();
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.CreateHelpRequest(resident.Id, helpRequestCommand);
            var createdRecord = DatabaseContext.HelpRequestEntities.Find(response);
            var expectedRecord = new HelpRequestEntity()
            {
                Id = response,
                ResidentId = resident.Id,
                IsOnBehalf = true,
                ConsentToCompleteOnBehalf = true,
                OnBehalfFirstName = "Tim",
                CallbackRequired = true
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord,options =>
            {
                options.Excluding(ex => ex.ResidentEntity);
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
            resident.Id.Should().Be(createdRecord.ResidentId);
        }

//        [Test]
//        public void CreateDuplicateHelpRequestTheLatestAsMaster()
//        {
//            var helpRequest = _fixture.Build<CreateHelpRequest>()
////                .With(x => x.Uprn, "123")
////                .With(x => x.DobMonth, "123")
////                .With(x => x.DobDay, "123")
////                .With(x => x.DobYear, "123")
////                .With(x => x.ContactTelephoneNumber, "123")
////                .With(x => x.ContactMobileNumber, "123")
//                .Create();
//
//            var helpRequest2 = _fixture.Build<CreateHelpRequest>()
////                .With(x => x.Uprn, "123")
////                .With(x => x.DobMonth, "123")
////                .With(x => x.DobDay, "123")
////                .With(x => x.DobYear, "123")
////                .With(x => x.ContactTelephoneNumber, "123")
////                .With(x => x.ContactMobileNumber, "123")
//                .Create();
//
//            var response1 = _classUnderTest.CreateHelpRequest(5, helpRequest);
//            var response2 = _classUnderTest.CreateHelpRequest(5, helpRequest2);
//            var firstRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response1);
//            var secondRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response2);
//            firstRecordToCheck.RecordStatus.Should().Be("DUPLICATE");
//            secondRecordToCheck.RecordStatus.Should().Be("MASTER");
//        }
//
//        [Test]
//        public void PatchHelpRequestForHelpNeeded()
//        {
//
//            var helpRequest = _fixture.Build<HelpRequestEntity>()
//                .Without(x => x.HelpRequestCalls)
//                .With(x => x.HelpWithCompletingNssForm, true)
//                .With(x => x.HelpWithShieldingGuidance, true)
//                .With(x => x.HelpWithNoNeedsIdentified, true)
//                .With(x => x.HelpWithAccessingSupermarketFood, false).Create();
//
//            var dbEntity = DatabaseContext.HelpRequestEntities.Add(helpRequest);
//            DatabaseContext.SaveChanges();
//            var patchRequestObject = new PatchHelpRequest()
//            {
//                HelpWithCompletingNssForm = false,
//                HelpWithShieldingGuidance = false,
//                HelpWithNoNeedsIdentified = null,
//                HelpWithAccessingSupermarketFood = false
//            };
//
//            _classUnderTest.PatchHelpRequest(helpRequest.Id, patchRequestObject);
//
//            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(helpRequest.Id);
//
//            updatedEntity.HelpWithCompletingNssForm.Should().Be(false);
//            updatedEntity.HelpWithShieldingGuidance.Should().Be(false);
//            updatedEntity.HelpWithNoNeedsIdentified.Should().Be(true);
//            updatedEntity.HelpWithAccessingSupermarketFood.Should().Be(false);
//        }
//
//        [Test]
//        public void GetHelpRequestReturnsEmptyCallsListIfNoCallsExist()
//        {
//            var id = 123;
//            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id));
//            DatabaseContext.SaveChanges();
//            var response = _classUnderTest.GetHelpRequest(id);
//            response.HelpRequestCalls.Should().BeNullOrEmpty();
//        }
//
//        [Test]
//        public void GetHelpRequestReturnsCallsListIfCallsExist()
//        {
//            var id = 124;
//            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id));
//            var calls = EntityHelpers.createHelpRequestCallEntities(3);
//            calls.ForEach(x => x.HelpRequestId = id);
//            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
//            DatabaseContext.SaveChanges();
//            var response = _classUnderTest.GetHelpRequest(id);
//            response.HelpRequestCalls.Count.Should().Be(3);
//            response.HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
//            {
//                options.Excluding(ex => ex.HelpRequestEntity);
//                return options;
//            });
//        }
//
//        [Test]
//        public void GetAllCallbacksWithCallsListIfCallsExist()
//        {
//            var id = 124;
//            var helpRequest = EntityHelpers.createHelpRequestEntity(id);
//            DatabaseContext.HelpRequestEntities.Add(helpRequest);
//            helpRequest.CallbackRequired = true;
//
//            var calls = EntityHelpers.createHelpRequestCallEntities(3);
//            calls.ForEach(x => x.HelpRequestId = id);
//            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
//            DatabaseContext.SaveChanges();
//            var response = _classUnderTest.GetCallbacks(new CallbackQuery() { HelpNeeded = "" });
//
//            response.First().HelpRequestCalls.Count.Should().Be(3);
//            response.First().HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
//            {
//                options.Excluding(ex => ex.HelpRequestEntity);
//                return options;
//            });
//        }
//
//        [Test]
//        public void SearchRequestsReturnsCallsListIfCallsExist()
//        {
//            var id = 124;
//            var helpRequestEntity = Randomm.Build<HelpRequestEntity>()
//                .With(x => x.Id, id)
//                .With(x => x.FirstName, "name")
//                .Without(h => h.HelpRequestCalls)
//                .Create();
//            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
//            var calls = EntityHelpers.createHelpRequestCallEntities(3);
//            calls.ForEach(x => x.HelpRequestId = id);
//            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
//            DatabaseContext.SaveChanges();
//            var response = _classUnderTest.SearchHelpRequests(new SearchRequest() { FirstName = "name" });
//            response.First().HelpRequestCalls.Count.Should().Be(3);
//            var callsDomain = calls.ToDomain();
//            response.First().HelpRequestCalls.Should().BeEquivalentTo(callsDomain);
//        }
//
//        [Test]
//        public void GetCallbacksReturnsCallbacks()
//        {
//            var helpRequests = EntityHelpers.createHelpRequestEntities();
//            foreach (var request in helpRequests)
//            {
//                request.InitialCallbackCompleted = true;
//                request.CallbackRequired = true;
//                request.RecordStatus = "MASTER";
//            }
//            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
//            DatabaseContext.SaveChanges();
//            var hrParams = new CallbackQuery() { HelpNeeded = null, Master = "True" };
//            var response = _classUnderTest.GetCallbacks(hrParams);
//            response.Should().BeEquivalentTo(helpRequests);
//        }
//
//        [Test]
//        public void GetCallbacksWithWhitespaceInRequestParamStillReturnsCallbacks()
//        {
//            var helpRequests = EntityHelpers.createHelpRequestEntities();
//            foreach (var request in helpRequests)
//            {
//                request.InitialCallbackCompleted = true;
//                request.CallbackRequired = true;
//                request.RecordStatus = "MASTER ";
//            }
//            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
//            DatabaseContext.SaveChanges();
//            var hrParams = new CallbackQuery() { HelpNeeded = null, Master = "True" };
//            var response = _classUnderTest.GetCallbacks(hrParams);
//            response.Should().BeEquivalentTo(helpRequests);
//        }

    }
}
