using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class PatchHelpRequest : IntegrationTests<Startup>
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task PatchResidentInformationWithPatchableFieldUpdatesTheRecord()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            var requestObject = DatabaseContext.HelpRequestEntities.First();
            requestObject.FirstName = "to-test-for";
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/{requestObject.Id}", UriKind.Relative);
            var response = Client.PatchAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var updatedEntity = DatabaseContext.HelpRequestEntities.First();
            updatedEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
        }

        [Test]
        public async Task PatchResidentInformationWithNonPatchableFieldDoesNotUpdateTheRecord()
        {
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            string changeValue = "to-test-for";
            var requestObject = DatabaseContext.HelpRequestEntities.First();
            var data = JsonConvert.SerializeObject(requestObject);
            data = data.Replace(requestObject.OnBehalfFirstName, changeValue,StringComparison.InvariantCulture);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/{requestObject.Id}", UriKind.Relative);
            var response = Client.PatchAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var updatedEntity = DatabaseContext.HelpRequestEntities.First();
            updatedEntity.OnBehalfFirstName.Should().NotBeEquivalentTo(changeValue);
        }
    }
}
