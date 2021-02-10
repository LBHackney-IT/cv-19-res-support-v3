using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Bogus;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V4.Helpers;
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
    public class SearchResident : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task SearchResidentsReturnsMatchingRecords()
        {
            var residents = EntityHelpers.CreateResidentEntities(10);
            DatabaseContext.ResidentEntities.AddRange(residents);
            DatabaseContext.SaveChanges();
            var uri = new Uri($"api/v4/residents?firstname={residents[0].FirstName}", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<ResidentResponseBoundary>>(stringContent);
            var residentEntity = DatabaseContext.ResidentEntities.FirstOrDefault(x => x.FirstName == residents[0].FirstName);
            convertedResponse.FirstOrDefault().Should().BeEquivalentTo(residentEntity.ToResidentDomain().ToResponse());
        }

        [Test]
        public async Task SearchResidentsWithoutMatchesReturnsEmptyResult()
        {
            var residents = EntityHelpers.CreateResidentEntities(10);
            DatabaseContext.ResidentEntities.AddRange(residents);
            DatabaseContext.SaveChanges();
            string irrelevantWord = "irrelevant";
            var uri = new Uri($"api/v4/residents?firstname={irrelevantWord}", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<ResidentResponseBoundary>>(stringContent);
            convertedResponse.Count.Should().Be(0);
        }
    }
}
