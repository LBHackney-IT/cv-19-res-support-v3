using System;
using System.Linq;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Moq;
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

            createdRecord.Should().BeEquivalentTo(expectedRecord, options =>
            {
                options.Excluding(ex => ex.ResidentEntity);
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
            resident.Id.Should().Be(createdRecord.ResidentId);
        }

        [Test]
        public void PatchHelpRequestForHelpNeeded()
        {
            var resident = EntityHelpers.createResident(116);
            var helpRequest = EntityHelpers.createHelpRequestEntity(114, resident.Id);
            helpRequest.HelpWithCompletingNssForm = true;
            helpRequest.HelpWithShieldingGuidance = true;
            helpRequest.HelpWithNoNeedsIdentified = true;
            helpRequest.HelpWithAccessingSupermarketFood = false;

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();
            var patchRequestObject = new PatchHelpRequest()
            {
                HelpWithCompletingNssForm = false,
                HelpWithShieldingGuidance = false,
                HelpWithNoNeedsIdentified = null,
                HelpWithAccessingSupermarketFood = false,
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
            var residentId = 111;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id, residentId));
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetHelpRequest(id);
            response.HelpRequestCalls.Should().BeNullOrEmpty();
        }

        [Test]
        public void GetHelpRequestReturnsCallsListIfCallsExist()
        {
            var id = 124;
            var residentId = 112;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(id, residentId));
            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = id);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetHelpRequest(id);
            response.HelpRequestCalls.Count.Should().Be(3);
            response.HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
            {
                options.Excluding(ex => ex.HelpRequestEntity);
                return options;
            });
        }

        [Test]
        public void GetHelpRequestReturnsHelpRequestIfItExist()
        {
            var id = 120;
            var residentId = 101;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            var helpRequestEntity = EntityHelpers.createHelpRequestEntity(id, residentId, callHandler: new CallHandlerEntity() { Id = 1, Name = "Test" });
            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetHelpRequest(id);
            response.Should().BeEquivalentTo(helpRequestEntity, options =>
            {
                options.Excluding(ex => ex.CaseNotes);
                options.Excluding(ex => ex.ResidentEntity);
                options.Excluding(ex => ex.CallHandlerEntity);
                options.Excluding(ex => ex.CallHandlerId);
                return options;
            });

            response.AssignedTo.Should().BeEquivalentTo(helpRequestEntity.CallHandlerEntity.Name);
        }

        [Test]
        public void SearchRequestsReturnsCallsListIfCallsExist()
        {
            var id = 124;
            var residentEntity = Randomm.Build<ResidentEntity>()
                .With(x => x.Id, id)
                .With(x => x.FirstName, "name")
                .Without(h => h.CaseNotes)
                .Without(h => h.HelpRequests)
                .Create();
            var helpRequestEntity = Randomm.Build<HelpRequestEntity>()
                .With(x => x.Id, 8)
                .With(x => x.ResidentId, residentEntity.Id)
                .Without(h => h.ResidentEntity)
                .Without(h => h.CaseNotes)
                .Without(h => h.HelpRequestCalls)
                .Create();
            DatabaseContext.ResidentEntities.Add(residentEntity);
            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = 8);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.SearchHelpRequests(new SearchRequest() { FirstName = "name" });
            response.First().HelpRequestCalls.Count.Should().Be(3);
            var callsDomain = calls.ToDomain();
            response.First().HelpRequestCalls.Should().BeEquivalentTo(callsDomain);
            response.First().ResidentId.Should().Be(id);
            response.First().Id.Should().Be(8);
        }

        [Test]
        public void GetCallbacksReturnsCallbacks()
        {
            var resident = EntityHelpers.createResident();
            var residentId = 809;
            resident.Id = residentId;
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            var id = 0;
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.HelpNeeded = "help request";
                request.ResidentId = residentId;
                request.CallHandlerEntity = new CallHandlerEntity() { Id = id, Name = "Jeremy" };
                id++;
            }

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var hrParams = new CallbackQuery() { HelpNeeded = "help request" };
            var response = _classUnderTest.GetCallbacks(hrParams);
            response.Should().BeEquivalentTo(helpRequests, options =>
            {
                options.Excluding(x => x.CaseNotes);
                options.Excluding(x => x.ResidentEntity);
                options.Excluding(x => x.CallHandlerEntity);
                options.Excluding(x => x.CallHandlerId);
                return options;
            });

            response.ForEach(r =>
            {
                r.AssignedTo.Should().BeEquivalentTo(helpRequests.FirstOrDefault(h => h.Id == r.Id).CallHandlerEntity.Name);
            });

            var hrParams2 = new CallbackQuery() { HelpNeeded = "something else" };
            var response2 = _classUnderTest.GetCallbacks(hrParams2);
            response2.Should().BeNullOrEmpty();
        }

        [Test]
        public void GetCallbacksWithWhitespaceInRequestParamStillReturnsCallbacks()
        {
            var resident = EntityHelpers.createResident();
            var residentId = 808;
            resident.Id = residentId;
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.HelpNeeded = "shielding ";
                request.ResidentId = residentId;
            }

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var hrParams = new CallbackQuery() { HelpNeeded = "shielding" };
            var response = _classUnderTest.GetCallbacks(hrParams);

            response.Count.Should().Be(helpRequests.Count);
        }

        [Test]
        public void GetAllCallbacksWithCallsListIfCallsExist()
        {
            var resident = EntityHelpers.createResident();
            var residentId = 808;
            resident.Id = residentId;
            var id = 124;
            var helpRequest = EntityHelpers.createHelpRequestEntity(id);
            helpRequest.CallbackRequired = true;
            helpRequest.ResidentId = residentId;
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            var calls = EntityHelpers.createHelpRequestCallEntities(3);
            calls.ForEach(x => x.HelpRequestId = id);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetCallbacks(new CallbackQuery() { HelpNeeded = "" });

            response.First().HelpRequestCalls.Count.Should().Be(3);
        }

        [Test]
        public void UpdateHelpRequestUpdatesCorrectFields()
        {
            var resident = EntityHelpers.createResident(117);
            var helpRequest = EntityHelpers.createHelpRequestEntity(117, resident.Id);
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();

            var updateRequestObject = Randomm.Build<UpdateHelpRequest>().Create();

            _classUnderTest.UpdateHelpRequest(helpRequest.Id, updateRequestObject);

            var oldEntity = DatabaseContext.HelpRequestEntities.Find(helpRequest.Id);
            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;
            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(helpRequest.Id);

            updatedEntity.Should().BeEquivalentTo(updateRequestObject, options =>
                {
                    options.Excluding(x => x.DateTimeRecorded);
                    return options;
                });
            updatedEntity.ResidentId.Should().Be(resident.Id);
            updatedEntity.Id.Should().Be(helpRequest.Id);
        }

        [Test]
        public void CanFindHelpRequestByCtasIdAndHelpNeeded()
        {
            var resident = EntityHelpers.createResident(118);
            var helpRequest = EntityHelpers.createHelpRequestEntity(117, resident.Id, "Welfare Call");
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();

            var response = _classUnderTest.FindHelpRequestByCtasId(helpRequest.NhsCtasId, helpRequest.HelpNeeded);

            response.Should().Be(117);
        }

        [Test]
        public void FindHelpRequestByCtasIdReturnsNullIfItDoesntExist()
        {
            var response = _classUnderTest.FindHelpRequestByCtasId("anything", "anything");

            response.Should().BeNull();
        }

        [Test]
        public void FindHelpRequestByCtasIdReturnsNullIfCtasIdExistsButHelpNeededDoesNotMatch()
        {
            var resident = EntityHelpers.createResident(118);
            var helpRequest = EntityHelpers.createHelpRequestEntity(117, resident.Id, "Welfare Call");
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();

            var response = _classUnderTest.FindHelpRequestByCtasId(helpRequest.NhsCtasId, "Shielding");

            response.Should().BeNull();
        }

        [Test]
        public void FindHelpRequestByMetadaReturnsNullIfItDoesntExist()
        {
            var response = _classUnderTest.FindHelpRequestByMetadata("someName", It.IsAny<object>());

            response.Should().BeNull();
        }
    }
}
