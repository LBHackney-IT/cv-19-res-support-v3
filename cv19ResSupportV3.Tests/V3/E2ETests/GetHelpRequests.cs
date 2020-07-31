using System;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helper;
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
        public void GetHelpRequestsWithInvalidIdsReturnsNotFound()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/{dbEntity.Entity.Id + 1}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }

    }
}
