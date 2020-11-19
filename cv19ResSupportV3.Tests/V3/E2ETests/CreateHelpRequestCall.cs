using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class CreateHelpRequestCall : IntegrationTests<Startup>
    {
        [Test]
        public async Task CreateHelpRequestCallReturnsTheCorrectInformation()
        {

            var requestObject = new Fixture().Build<HelpRequestCall>().Create();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-request-call", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCallCreateResponse>(stringContent);
//            var createdEntity = DatabaseContext.HelpRequestCall.First();
//            createdEntity.helpRequestId.Should().BeEquivalentTo(requestObject.helpRequestId);
//            createdEntity.callType.Should().BeEquivalentTo(requestObject.callType);
//            createdEntity.callOutcome.Should().BeEquivalentTo(requestObject.callOutcome);
//            createdEntity.callDateTime.Should().BeEquivalentTo(requestObject.callDateTime);

        }
    }
}
