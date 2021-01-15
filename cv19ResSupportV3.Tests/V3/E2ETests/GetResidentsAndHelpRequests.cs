using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class GetResidentsAndHelpRequests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            CustomizeAssertions.ApproximationDateTime();
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task GetHelpRequestsWithValidIdsReturnsTheCorrectInformation()
        {
            var residentId = 500;
            var postcode = "ABC";
            var residentEntity = Randomm.Build<ResidentEntity>()
                                        .With(x => x.Id, residentId)
                                        .With(x => x.PostCode, postcode)
                                        .Without(h => h.CaseNotes)
                                        .Without(h => h.HelpRequests)
                                        .Create();
            var residentEntity2 = EntityHelpers.createResident(109);
            DatabaseContext.ResidentEntities.AddRange(residentEntity, residentEntity2);
            var helpRequestEntity = EntityHelpers.createHelpRequestEntity(235, residentId);
            var helpRequestEntity2 = EntityHelpers.createHelpRequestEntity(236, residentId);
            var helpRequestEntity3 = EntityHelpers.createHelpRequestEntity(237, residentEntity2.Id);
            DatabaseContext.HelpRequestEntities.AddRange(helpRequestEntity, helpRequestEntity2, helpRequestEntity3);
            DatabaseContext.SaveChanges();

            var expectedResponse1 = residentEntity.ToDomain(helpRequestEntity);
            var expectedResponse2 = residentEntity.ToDomain(helpRequestEntity2);
            var requestUri = new Uri($"api/v3/help-requests?PostCode=ABC", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<HelpRequestResponse>>(stringContent);
            convertedResponse.First().Should().BeEquivalentTo(expectedResponse1, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
            convertedResponse[1].Should().BeEquivalentTo(expectedResponse2, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
            convertedResponse.Count.Should().Be(2);
        }
    }
}
