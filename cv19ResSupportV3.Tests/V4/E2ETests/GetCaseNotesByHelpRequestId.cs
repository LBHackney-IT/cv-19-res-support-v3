using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4.Boundary.Responses;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class GetCaseNotesByHelpRequestId : IntegrationTests<Startup>
    {
        Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task GetsCaseNotesByHelpRequestId()
        {
            var resident = _fixture.Build<ResidentEntity>()
                .Without(re => re.HelpRequests)
                .Without(re => re.CaseNotes)
                .Create();

            var helpRequest = _fixture.Build<HelpRequestEntity>()
                .Without(hr => hr.ResidentEntity)
                .Without(hr => hr.CaseNotes)
                .Without(hr => hr.HelpRequestCalls)
                .With(hr => hr.ResidentId, resident.Id)
                .Create();

            var caseNotes = _fixture.Build<CaseNoteEntity>()
                .With(x => x.ResidentId, resident.Id)
                .Without(x => x.ResidentEntity)
                .Without(x => x.HelpRequestEntity)
                .With(x => x.HelpRequestId, helpRequest.Id)
                .CreateMany().ToList();
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            DatabaseContext.CaseNoteEntities.AddRange(caseNotes);
            DatabaseContext.SaveChanges();

            var uri = new Uri($"api/v4/residents/{resident.Id}/help-requests/{helpRequest.Id}/case-notes", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<CaseNoteResponse>>(stringContent);
            convertedResponse.Count.Should().Be(caseNotes.Count);
        }
    }
}
