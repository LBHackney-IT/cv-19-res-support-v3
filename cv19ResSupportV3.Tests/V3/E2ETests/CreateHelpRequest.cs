using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V1.E2ETests
{
    [TestFixture]
    public class CreateHelpRequest : IntegrationTests<Startup>
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        // [Test]
        public void GetResidentInformationByIdReturnsTheCorrectInformation()
        {
            var data = "{\"CustomerId\": 5,\"CustomerName\": \"Pepsi\"}";
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);

            var content = response.Result.Content;
            //var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            // var convertedResponse = JsonConvert.DeserializeObject<ResidentInformation>(stringContent);
            //
            // convertedResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void GetResidentByIdReturns400IfBadRequest()
        {
            var data = "{\"CustomerId\": 5,\"CustomerName\": \"Pepsi\"}";
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-request", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }
    }
}
