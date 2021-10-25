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
    public class DeleteCallHandlerTests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task DeleteCallHandlerRequestDeletesRecordAndUnassigns()
        {
            int testId = 1;

            // Arrange
            DatabaseContext.ResidentEntities.Add(new ResidentEntity() { Id = 1 });
            DatabaseContext.CallHandlerEntities.Add(new CallHandlerEntity() { Id = testId });
            DatabaseContext.HelpRequestEntities.Add(new HelpRequestEntity() { CallHandlerId = testId, ResidentId = 1 });
            DatabaseContext.SaveChanges();

            var uri = new Uri($"api/v4/call-handlers/{testId}", UriKind.Relative);

            // Act
            var response = await Client.DeleteAsync(uri).ConfigureAwait(true);
            var statusCode = response.StatusCode;
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);

            // Assert
            statusCode.Should().Be(200);

            var callHandlerEntity = DatabaseContext.CallHandlerEntities.FirstOrDefault();
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.FirstOrDefault(x => x.CallHandlerId == testId);

            callHandlerEntity.Should().BeNull();
            helpRequestEntity.Should().BeNull();
        }

        [Test]
        public async Task DeleteCallHandlerRequestWithInvalidIdReturnsBadRequestAndDoesNotDelete()
        {
            int testId = 1;

            // Arrange
            DatabaseContext.ResidentEntities.Add(new ResidentEntity() { Id = 1 });
            DatabaseContext.CallHandlerEntities.Add(new CallHandlerEntity() { Id = testId });
            DatabaseContext.HelpRequestEntities.Add(new HelpRequestEntity() { CallHandlerId = testId, ResidentId = 1 });
            DatabaseContext.SaveChanges();

            var uri = new Uri($"api/v4/call-handlers/2", UriKind.Relative);

            // Act
            var response = await Client.DeleteAsync(uri).ConfigureAwait(true);
            var statusCode = response.StatusCode;
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);

            // Assert
            statusCode.Should().Be(400);

            var callHandlerEntity = DatabaseContext.CallHandlerEntities.FirstOrDefault();
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.FirstOrDefault(x => x.CallHandlerId == testId);

            callHandlerEntity.Should().NotBeNull();
            helpRequestEntity.Should().NotBeNull();
        }
    }
}
