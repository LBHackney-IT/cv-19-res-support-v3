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
    public class GetHelpRequests : IntegrationTests<Startup>
    {
        private Fixture _fixture = new Fixture();

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
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            var expectedResponse = dbEntity.Entity.ToResponse();
            var requestUri = new Uri($"api/v3/help-requests/{dbEntity.Entity.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestGetResponse>(stringContent);
            convertedResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetHelpRequestsWithoutIdsReturnsNothing()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
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
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/{dbEntity.Entity.Id + 1}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }

        [Test]
        public async Task GetHelpRequestsWithValidPostCodeReturnsRecord()
        {
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
            var helpRequests = _fixture.CreateMany<HelpRequestEntity>().ToList();
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
    }
}
