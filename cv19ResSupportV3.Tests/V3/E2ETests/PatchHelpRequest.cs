using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            var residentId = 23;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(12, residentId));
            DatabaseContext.SaveChanges();
            var requestObject = helpRequestEntity.Entity.ToDomain();
            requestObject.FirstName = "to-test-for";
            requestObject.HelpNeeded = "changed help needed";
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
            var oldHelpRequestEntity = DatabaseContext.HelpRequestEntities.Find(helpRequestEntity.Entity.Id);
            var oldResidentEntity = DatabaseContext.ResidentEntities.Find(residentId);
            DatabaseContext.Entry(oldHelpRequestEntity).State = EntityState.Detached;
            DatabaseContext.Entry(oldResidentEntity).State = EntityState.Detached;
            var updatedHelpRequestEntity = DatabaseContext.HelpRequestEntities.Find(helpRequestEntity.Entity.Id);
            var updatedResidentEntity = DatabaseContext.ResidentEntities.Find(residentId);

            updatedResidentEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
            updatedHelpRequestEntity.HelpNeeded.Should().BeEquivalentTo(requestObject.HelpNeeded);
        }


        [Test]
        public async Task PatchResidentInformationWithPatchableAddressFieldUpdatesTheRecord()
        {
            var residentId = 24;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(13, residentId));
            DatabaseContext.SaveChanges();
            var requestObject = DatabaseContext.HelpRequestEntities.Find(13).ToDomain();
            requestObject.AddressFirstLine = "7 test road";
            requestObject.AddressSecondLine = null;
            requestObject.AddressThirdLine = null;
            requestObject.PostCode = "ABC 123";
            requestObject.Uprn = "1231456456789";

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
            var oldResidentEntity = DatabaseContext.ResidentEntities.Find(residentId);
            DatabaseContext.Entry(oldResidentEntity).State = EntityState.Detached;
            var updatedEntity = DatabaseContext.ResidentEntities.Find(residentId);
            updatedEntity.AddressFirstLine.Should().BeEquivalentTo(requestObject.AddressFirstLine);
            updatedEntity.AddressSecondLine.Should().BeNullOrEmpty();
            updatedEntity.AddressThirdLine.Should().BeNullOrEmpty();
            updatedEntity.PostCode.Should().BeEquivalentTo(requestObject.PostCode);
            updatedEntity.Uprn.Should().BeEquivalentTo(requestObject.Uprn);
        }

        [Test]
        public async Task PatchResidentInformationWithHelpNeededFieldUpdatesTheRecord()
        {
            var residentId = 24;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            DatabaseContext.HelpRequestEntities.Add(new Fixture().Build<HelpRequestEntity>().
                Without(x => x.HelpRequestCalls).
                Without(x => x.ResidentEntity).
                Without(x => x.CaseNotes).
                With(x => x.Id, 1).
                With(x => x.ResidentId, residentId).
                With(x => x.HelpWithCompletingNssForm, true).
                With(x => x.HelpWithShieldingGuidance, true).
                With(x => x.HelpWithNoNeedsIdentified, true).
                With(x => x.HelpWithAccessingSupermarketFood, false).Create());
            DatabaseContext.SaveChanges();
            var requestObject = DatabaseContext.HelpRequestEntities.Find(1);

            var patchRequestObject = new HelpRequestEntity()
            {
                HelpWithCompletingNssForm = false,
                HelpWithShieldingGuidance = false,
                HelpWithNoNeedsIdentified = null,
                HelpWithAccessingSupermarketFood = true,
                Id = requestObject.Id,
            }.ToDomain();

            var data = JsonConvert.SerializeObject(patchRequestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/{requestObject.Id}", UriKind.Relative);
            var response = Client.PatchAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequest>(stringContent);
            var oldEntity = DatabaseContext.HelpRequestEntities.Find(requestObject.Id);
            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;
            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(requestObject.Id);
            updatedEntity.HelpWithCompletingNssForm.Should().Be(false);
            updatedEntity.HelpWithShieldingGuidance.Should().Be(false);
            updatedEntity.HelpWithNoNeedsIdentified.Should().Be(true);
            updatedEntity.HelpWithAccessingSupermarketFood.Should().Be(true);
        }

        [Test]
        public async Task PatchResidentInformationWithNonPatchableFieldDoesNotUpdateTheRecord()
        {
            var residentId = 24;
            DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident(residentId));
            var dbEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(37, residentId));
            DatabaseContext.SaveChanges();
            string changeValue = "to-test-for";
            var requestObject = DatabaseContext.HelpRequestEntities.Find(dbEntity.Entity.Id).ToDomain();
            var data = JsonConvert.SerializeObject(requestObject);
            data = data.Replace(requestObject.OnBehalfFirstName, changeValue, StringComparison.InvariantCulture);
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
