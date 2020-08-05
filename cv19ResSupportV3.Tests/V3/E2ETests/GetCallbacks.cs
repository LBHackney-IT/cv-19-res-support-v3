using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class GetCallbacks : IntegrationTests<Startup>
    {
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            CustomizeAssertions.ApproximationDateTime();
            DatabaseContext.Database.RollbackTransaction();
            ClearTable();
        }


        [Test]
        public async Task GetCallbacksReturnsCallbacks()
        {
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
            foreach (var request in helpRequests)
            {
                request.CallbackRequired = true;
                request.InitialCallbackCompleted = true;
                request.DateTimeRecorded = DateTime.Today.AddDays(-2);
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/callbacks", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Count().Should().Be(helpRequests.Count);
            deserializedBody.Count.Should().Be(helpRequests.Count);
        }

        [Test]
        public async Task GetCallbacksWithNoCallbacksRecordedReturnsNothing()
        {
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = false;
                request.DateTimeRecorded = DateTime.Today.AddDays(-2);
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/callbacks", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Count().Should().Be(helpRequests.Count);
            deserializedBody.Count.Should().Be(0);
        }

        [Test]
        public async Task GetCallbacksMasterRecordsReturnsOnlyMasterRecords()
        {
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
            int count = 0;
            foreach (var request in helpRequests)
            {
                request.InitialCallbackCompleted = true;
                request.CallbackRequired = true;
                request.DateTimeRecorded = DateTime.Today.AddDays(-2);
                if (count % 2 == 0)
                {
                    request.RecordStatus = "MASTER";
                }
                else
                {
                    request.RecordStatus = "DUPLICATE";
                }
                count++;
            }
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            DatabaseContext.HelpRequestEntities.Count().Should().Be(helpRequests.Count);
            var requestUri = new Uri($"api/v3/help-requests/callbacks?master=true", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            int expectedResponse = helpRequests.Count(a => a.RecordStatus == "MASTER");
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(expectedResponse);
        }

        private void ClearTable()
        {
            var addedEntities = DatabaseContext.HelpRequestEntities;
            DatabaseContext.HelpRequestEntities.RemoveRange(addedEntities);
            DatabaseContext.SaveChanges();
        }
    }
}
