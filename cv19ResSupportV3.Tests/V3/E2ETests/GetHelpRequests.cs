using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class GetHelpRequests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            CustomizeAssertions.ApproximationDateTime();
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task GetHelpRequestsWithValidIdsReturnsTheCorrectInformation()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity());
            DatabaseContext.SaveChanges();
            var expectedResponse = dbEntity.Entity;
            var requestUri = new Uri($"api/v3/help-requests/{dbEntity.Entity.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestGetResponse>(stringContent);
            convertedResponse.Should().BeEquivalentTo(expectedResponse, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                options.Excluding(ex => ex.NhsNumber);
                return options;
            });
        }

        [Test]
        public async Task GetHelpRequestsWithoutIdsReturnsNothing()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity());
            DatabaseContext.SaveChanges();
            var expectedResponse = "[]";
            var requestUri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            stringContent.Should().Be(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithAnValidFirstNameAndOtherParamsValidReturnsNothing()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().PostCode = "to-search-for";
            helpRequests.First().FirstName = "to-search-for";
            helpRequests.First().LastName = "to-search-for";
            string invalidFirstName = "not-valid";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?postcode={helpRequests.First().PostCode}&firstname={invalidFirstName}&lastname={helpRequests.First().LastName}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = new HelpRequestGetResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(0);
        }

        [Test]
        public void GetHelpRequestsWithInvalidIdsReturnsNotFound()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity());
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/{dbEntity.Entity.Id + 1}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }

        [Test]
        public async Task GetMultipleHelpRequestsReturnsTheCorrectInformation()
        {
            // arrange
            var firstName = "to-search-for";

            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.ForEach(r => r.FirstName = firstName);

            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();

            // act
            var requestUri = new Uri(
                $"api/v3/help-requests?firstname={firstName}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);

            // assert
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);

            deserializedBody.Should().BeEquivalentTo(helpRequests, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                options.Excluding(ex => ex.NhsNumber);
                return options;
            });
        }

        [Test]
        public async Task GetHelpRequestsWithValidPostCodeReturnsRecord()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().PostCode = "to-search-for";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?postcode={helpRequests.First().PostCode}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First().ToResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithValidFirstNameReturnsRecord()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().FirstName = "to-search-for";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?firstname={helpRequests.First().FirstName}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First().ToResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithValidLastNameReturnsRecord()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().LastName = "to-search-for";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?lastname={helpRequests.First().LastName}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First().ToResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithValidPostCodeFirstNameAndLastNameReturnsRecord()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().PostCode = "to-search-for";
            helpRequests.First().FirstName = "to-search-for";
            helpRequests.First().LastName = "to-search-for";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?postcode={helpRequests.First().PostCode}&firstname={helpRequests.First().FirstName}&lastname={helpRequests.First().LastName}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First().ToResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithValidPostCodePartialReturnsRecord()
        {
            var helpRequests = EntityHelpers.createHelpRequestEntities();
            helpRequests.First().PostCode = "to-search-for";
            DatabaseContext.HelpRequestEntities.AddRange(helpRequests);
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests?postcode=search", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var expectedResponse = helpRequests.First().ToResponse();
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.Count.Should().Be(1);
            deserializedBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestWithCalls()
        {
            var helpRequest = EntityHelpers.createHelpRequestEntity();
            var calls = EntityHelpers.createHelpRequestCallEntities();
            helpRequest.CallbackRequired = true;
            helpRequest.InitialCallbackCompleted = true;
            helpRequest.DateTimeRecorded = DateTime.Today.AddDays(-2);
            calls.ForEach(x => x.HelpRequestId = helpRequest.Id);
            helpRequest.HelpRequestCalls = calls;
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();

            var requestUri = new Uri($"api/v3/help-requests/{helpRequest.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<HelpRequestGetResponse>(stringResponse);
            deserializedBody.HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
            {
                options.Excluding(ex => ex.HelpRequestEntity);
                return options;
            });
        }


        [Test]
        public async Task SearchHelpRequestsReturnsHelpRequestsWithCalls()
        {
            var helpRequest = EntityHelpers.createHelpRequestEntity();
            var calls = EntityHelpers.createHelpRequestCallEntities();
            helpRequest.CallbackRequired = true;
            helpRequest.InitialCallbackCompleted = true;
            helpRequest.DateTimeRecorded = DateTime.Today.AddDays(-2);
            helpRequest.FirstName = "Sample";
            calls.ForEach(x => x.HelpRequestId = helpRequest.Id);
            helpRequest.HelpRequestCalls = calls;
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.HelpRequestCallEntities.AddRange(calls);
            DatabaseContext.SaveChanges();

            var requestUri = new Uri($"api/v3/help-requests?firstname=Sample", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<HelpRequestGetResponse>>(stringResponse);
            deserializedBody.First().HelpRequestCalls.Count.Should().Be(3);
            deserializedBody.First().HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
            {
                options.Excluding(ex => ex.HelpRequestEntity);
                return options;
            });
        }
    }
}
