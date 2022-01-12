using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class CreateCallHandlerTests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task CreateCallHandlerRequestCreatesRecord()
        {
            // Arrange
            var requestObject = new Fixture().Create<CreateCallHandlerRequestBoundary>();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/call-handlers", UriKind.Relative);

            // Act
            var response = await Client.PostAsync(uri, postContent).ConfigureAwait(true);
            postContent.Dispose();
            var statusCode = response.StatusCode;
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);

            // Assert
            statusCode.Should().Be(201);
            var convertedResponse = JsonConvert.DeserializeObject<CallHandlerResponseBoundary>(stringContent);
            var callHandlerEntity = DatabaseContext.CallHandlerEntities.FirstOrDefault();
            convertedResponse.Should().BeEquivalentTo(callHandlerEntity.ToDomain().ToResponse());
        }

        [Test]
        public void CreateCallHandlerRequestWithInvalidRequestObjectReturnsBadRequest()
        {
            // Arrange
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/call-handlers", UriKind.Relative);

            // Act
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();

            // Assert
            response.Result.StatusCode.Should().Be(400);
        }
    }
}
