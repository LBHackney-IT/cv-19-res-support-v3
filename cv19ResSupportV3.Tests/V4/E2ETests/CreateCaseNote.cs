using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Responses;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class CreateCaseNote : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task CanCreateCaseNotes()
        {
            var residentId = 1;
            var helpRequestId = 1;
            var resident = new Fixture().Build<ResidentEntity>()
                .Without(re => re.HelpRequests)
                .Without(re => re.CaseNotes)
                .With(x => x.Id,residentId)
                .Create();

            var helpRequest = new Fixture().Build<HelpRequestEntity>()
                .Without(hr => hr.ResidentEntity)
                .Without(hr => hr.HelpRequestCalls)
                .Without(hr => hr.CaseNotes)
                .With(x => x.Id,helpRequestId)
                .With(x => x.ResidentId,residentId)
                .Create();

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.SaveChanges();

            var caseNote = new CreateCaseNoteRequest() {CaseNote = "Content"};
            var data = JsonConvert.SerializeObject(caseNote);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/residents/1/help-requests/1/case-notes", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<CaseNoteResponse>(stringContent);
            var caseNoteEntity = DatabaseContext.CaseNoteEntities.FirstOrDefault(x => x.HelpRequestId == helpRequestId);
            convertedResponse.CaseNote.Should().Be(caseNoteEntity.CaseNote);
            caseNote.CaseNote.Should().Be(caseNoteEntity.CaseNote);
        }
    }
}
