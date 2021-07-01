using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
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
    public class UpdateHelpRequest : IntegrationTests<Startup>
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }
        [Test]
        public async Task UpdateResidentInformationUpdatesTheRecord()
        {
            var residentEntity = DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident());
            var helpReqestEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(123, residentEntity.Entity.Id));
            DatabaseContext.SaveChanges();
            var requestObject = residentEntity.Entity.ToDomain(helpReqestEntity.Entity);
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
            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(requestObject.Id);
            var oldEntity = DatabaseContext.ResidentEntities.Find(updatedEntity.ResidentId);
            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;
            var updatedResidentEntity = DatabaseContext.ResidentEntities.Find(updatedEntity.ResidentId);
            updatedResidentEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
        }

        [Test]
        public async Task UpdateHelpNeededFieldsUpdatesTheRecord()
        {

            var resident = EntityHelpers.createResident(1);
            var dbEntity = EntityHelpers.createHelpRequestEntity(1, resident.Id);
            dbEntity.HelpWithCompletingNssForm = true;
            dbEntity.HelpWithShieldingGuidance = true;
            dbEntity.HelpWithNoNeedsIdentified = true;
            dbEntity.HelpWithAccessingSupermarketFood = false;

            DatabaseContext.ResidentEntities.Add(resident);

            DatabaseContext.HelpRequestEntities.Add(dbEntity);

            var requestObject = DatabaseContext.HelpRequestEntities.Find(1);

            DatabaseContext.SaveChanges();

            var updateRequestObject = new HelpRequestUpdateRequest
            {
                HelpWithCompletingNssForm = false,
                HelpWithShieldingGuidance = false,
                HelpWithNoNeedsIdentified = null,
                HelpWithAccessingSupermarketFood = true,
                Id = requestObject.Id,
                HelpNeeded = requestObject.HelpNeeded,
            };

            var data = JsonConvert.SerializeObject(updateRequestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PutAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var oldEntity = DatabaseContext.HelpRequestEntities.Find(requestObject.Id);

            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;

            var updatedEntity = DatabaseContext.HelpRequestEntities.Find(requestObject.Id);
            updatedEntity.HelpWithCompletingNssForm.Should().Be(false);
            updatedEntity.HelpWithShieldingGuidance.Should().Be(false);
            updatedEntity.HelpWithNoNeedsIdentified.Should().Be(null);
            updatedEntity.HelpWithAccessingSupermarketFood.Should().Be(true);
        }
    }
}
