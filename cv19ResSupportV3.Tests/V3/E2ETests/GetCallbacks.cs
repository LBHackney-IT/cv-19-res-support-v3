using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class GetCallbacks : IntegrationTests<Startup>
    {
        ResidentEntity resident = EntityHelpers.createResident();

        [SetUp]
        public void SetUp()
        {
            CustomizeAssertions.ApproximationDateTime();
            DatabaseContext.Database.RollbackTransaction();
            DatabaseContext.ResidentEntities.Add(resident);
            E2ETestHelpers.ClearTable(DatabaseContext);
        }


        [Test]
        public async Task GetCallbacksReturnsCallbacks()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.CallbackRequired = true;
                request.InitialCallbackCompleted = true;
                request.DateTimeRecorded = DateTime.Today.AddDays(-2);
                request.ResidentId = resident.Id;
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/callbacks", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Count().Should().Be(helpRequests.Count);
            deserializedBody.Count.Should().Be(helpRequests.Count);
        }

        [Test]
        public async Task GetCallbacksWithNoCallbacksRecordedReturnsNothing()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = false;
                request.DateTimeRecorded = DateTime.Today.AddDays(-2);
                request.ResidentId = resident.Id;
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/callbacks", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Count().Should().Be(helpRequests.Count);
            deserializedBody.Count.Should().Be(0);
        }


        [Test]
        public async Task GetCallbacksMasterWithWhiteSpaceStillReturnsRecords()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.HelpNeeded = "help ";
                request.ResidentId = resident.Id;
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/callbacks?helpneeded=help", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(helpRequests.Count);
        }

        [Test]
        public async Task GetCallbacksReturnsCallbacksWithHelpRequestCalls()
        {
            var helpRequest = EntityHelpers.createHelpRequestEntity();
            var calls = EntityHelpers.createHelpRequestCallEntities();
            helpRequest.Id = 99;
            helpRequest.CallbackRequired = true;
            helpRequest.InitialCallbackCompleted = true;
            helpRequest.DateTimeRecorded = DateTime.Today.AddDays(-2);
            helpRequest.ResidentId = resident.Id;

            calls.ForEach(x => x.HelpRequestId = helpRequest.Id);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();

            var requestUri = new Uri($"api/v3/help-requests/callbacks", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Count().Should().Be(1);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.First().HelpRequestCalls.Count.Should().Be(3);
            deserializedBody.First().HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
            {
                options.Excluding(ex => ex.HelpRequestEntity);
                return options;
            });
        }
    }
}
