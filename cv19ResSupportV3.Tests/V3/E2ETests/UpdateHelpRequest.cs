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
    public class UpdateHelpRequest : IntegrationTests<Startup>
    {
        [Test]
        public async Task UpdateResidentInformationUpdatesTheRecord()
        {
            DatabaseContext.Database.RollbackTransaction();
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().Create());
            DatabaseContext.SaveChanges();
            var requestObject = DatabaseContext.HelpRequestEntities.First();
            requestObject.FirstName = "to-test-for";
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PutAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var updatedEntity = DatabaseContext.HelpRequestEntities.First();
            updatedEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
            DatabaseContext.Database.BeginTransaction();
        }
    }
}
