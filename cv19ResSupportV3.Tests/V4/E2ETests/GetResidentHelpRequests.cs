using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class GetResidentHelpRequests: IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }
        [Test]
        public async Task GetResidentHelpRequestsRequestGetsRecordWithCaseNotes()
        {
            var residentEntity = EntityHelpers.createResident(114);
            var helpRequestEntity = EntityHelpers.createHelpRequestEntity(43, residentEntity.Id);
            var caseNoteEntity = new CaseNoteEntity { CaseNote = "before update", ResidentId = residentEntity.Id, HelpRequestId = helpRequestEntity.Id };


            DatabaseContext.ResidentEntities.Add(residentEntity);
            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
            DatabaseContext.CaseNoteEntities.Add(caseNoteEntity);

            DatabaseContext.SaveChanges();

            var expectedResponse = helpRequestEntity.ToRequestResponse(residentEntity);

            var requestUri = new Uri($"api/v4/residents/{residentEntity.Id}/help-requests/{helpRequestEntity.Id}", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;

            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentHelpRequestResponse>(stringContent);

            convertedResponse.Should().BeEquivalentTo(expectedResponse, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                options.Excluding(ex => ex.DateTimeRecorded);
                return options;
            });
            Assert.That(convertedResponse.DateTimeRecorded, Is.EqualTo(expectedResponse.DateTimeRecorded).Within(1).Seconds);
        }
    }
}
