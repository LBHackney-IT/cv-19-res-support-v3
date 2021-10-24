using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class PutCallHandlerTests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task PutCallHandlerRequestUpdatesRecord()
        {
            const int _testId = 999;

            // Arrange
            var requestEntity = DatabaseContext.CallHandlerEntities.Add(new CallHandlerEntity() { Id = _testId, Name = "Aragorn" });
            DatabaseContext.SaveChanges();
            var requestObject = new Fixture().Create<PutCallHandlerRequestBoundary>();
            requestObject.Id = _testId;
            requestObject.Name = "Gandalf";

            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/call-handlers", UriKind.Relative);

            // Act
            var response = await Client.PutAsync(uri, postContent).ConfigureAwait(true);
            postContent.Dispose();
            var statusCode = response.StatusCode;
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);

            // Assert
            statusCode.Should().Be(200);
            var convertedResponse = JsonConvert.DeserializeObject<CallHandlerResponseBoundary>(stringContent);
            var callHandlerEntity = DatabaseContext.CallHandlerEntities.FirstOrDefault(x => x.Id == _testId);
            convertedResponse.Should().BeEquivalentTo(requestObject);
        }

        [Test]
        public void PutCallHandlerRequestWithInvalidRequestObjectReturnsBadRequest()
        {
            // Arrange
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/call-handlers", UriKind.Relative);

            // Act
            var response = Client.PutAsync(uri, postContent);
            postContent.Dispose();

            // Assert
            response.Result.StatusCode.Should().Be(400);
        }
    }
}
