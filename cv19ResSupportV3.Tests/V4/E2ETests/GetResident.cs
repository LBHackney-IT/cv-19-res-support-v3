using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class GetResident : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task CreateResidentRequestCreatesRecord()
        {
            var resident = new Fixture().Build<ResidentEntity>()
                .Without(re => re.HelpRequests)
                .Without(re => re.CaseNotes)
                .Create();
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.SaveChanges();
            var uri = new Uri($"api/v4/residents/{resident.Id}", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponseBoundary>(stringContent);
            var residentEntity = DatabaseContext.ResidentEntities.FirstOrDefault();
            //replace with a full object comparison once we have a resident toresponse factory method
            convertedResponse.FirstName.Should().Be(residentEntity.FirstName);
            convertedResponse.LastName.Should().Be(residentEntity.LastName);
        }
    }
}
