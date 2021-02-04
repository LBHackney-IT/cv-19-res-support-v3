using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class CreateHelpRequest : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task GetResidentInformationByIdReturnsTheCorrectInformation()
        {
            var requestObject = new Fixture().Create<HelpRequestCreateRequestBoundary>();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.Find(convertedResponse.Id);
            var residentEntity = DatabaseContext.ResidentEntities.Find(helpRequestEntity.ResidentId);
            residentEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
            residentEntity.LastName.Should().BeEquivalentTo(requestObject.LastName);
            residentEntity.Uprn.Should().BeEquivalentTo(requestObject.Uprn);
            helpRequestEntity.HelpWithAccessingSupermarketFood.Should().Be(requestObject.HelpWithAccessingSupermarketFood);
            helpRequestEntity.HelpWithCompletingNssForm.Should().Be(requestObject.HelpWithCompletingNssForm);
            helpRequestEntity.HelpWithShieldingGuidance.Should().Be(requestObject.HelpWithShieldingGuidance);
            helpRequestEntity.HelpWithNoNeedsIdentified.Should().Be(requestObject.HelpWithNoNeedsIdentified);
        }
        [Test]
        public async Task CreateHelpRequestAndUpdateExistingResidentInformation()
        {
            var existingResident = EntityHelpers.createResident(964);

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();

            var requestObject = new Fixture().Create<HelpRequestCreateRequestBoundary>();
            requestObject.FirstName = existingResident.FirstName;
            requestObject.LastName = existingResident.LastName;
            requestObject.Uprn = existingResident.Uprn;
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var oldResidentEntity = DatabaseContext.ResidentEntities.Find(existingResident.Id);
            DatabaseContext.Entry(oldResidentEntity).State = EntityState.Detached;
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.Find(convertedResponse.Id);
            var residentEntity = DatabaseContext.ResidentEntities.Find(helpRequestEntity.ResidentId);
            residentEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
            residentEntity.LastName.Should().BeEquivalentTo(requestObject.LastName);
            residentEntity.Uprn.Should().BeEquivalentTo(requestObject.Uprn);
            residentEntity.AddressFirstLine.Should().BeEquivalentTo(requestObject.AddressFirstLine);
            residentEntity.ContactMobileNumber.Should()
                .BeEquivalentTo(existingResident.ContactMobileNumber + " / " + requestObject.ContactMobileNumber);
            residentEntity.ContactTelephoneNumber.Should()
                .BeEquivalentTo(existingResident.ContactTelephoneNumber + " / " + requestObject.ContactTelephoneNumber);
            helpRequestEntity.HelpWithAccessingSupermarketFood.Should().Be(requestObject.HelpWithAccessingSupermarketFood);
            helpRequestEntity.HelpWithCompletingNssForm.Should().Be(requestObject.HelpWithCompletingNssForm);
            helpRequestEntity.HelpWithShieldingGuidance.Should().Be(requestObject.HelpWithShieldingGuidance);
            helpRequestEntity.HelpWithNoNeedsIdentified.Should().Be(requestObject.HelpWithNoNeedsIdentified);
        }

        [Test]
        public async Task CreateResidentAndHelpRequestWithMetadata()
        {
            // var requestObject = new Fixture().Create<HelpRequestCreateRequestBoundary>();
            var requestObject = "{";
            requestObject += "\"Postcode\": \"E8 1DY\",";
            requestObject += "\"AddressFirstLine\": \"1 Hillman street\",";
            requestObject += "\"FirstName\": \"Testy\",";
            requestObject += "\"LastName\": \"Testington\",";
            requestObject += "\"DobDay\": \"20\",";
            requestObject += "\"DobMonth\": \"04\",";
            requestObject += "\"DobYear\": \"1996\",";
            requestObject += "\"ContactTelephoneNumber\": \"1234567890\",";
            requestObject += "\"ContactMobileNumber\":  \"0987654321\",";
            requestObject += "\"EmailAddress\":  \"testy@test.com\",";
            requestObject += "\"CaseNotes\": \"Got covid\",";
            requestObject += "\"HelpNeeded\": \"Contact Tracing\",";
            requestObject += "\"NhsNumber\": \"11112222333\",";
            requestObject += "\"NhsCtasId\": \"abc123ef\",";
            requestObject += "\"Metadata\": { \"tested_date\": \"02-02-2020\" }";
            requestObject += "}";
            HttpContent postContent = new StringContent(requestObject, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<HelpRequestCreateResponse>(stringContent);
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.Find(convertedResponse.Id);
            var residentEntity = DatabaseContext.ResidentEntities.Find(helpRequestEntity.ResidentId);
            residentEntity.FirstName.Should().BeEquivalentTo("Testy");
            var helpRequestMetadata = (string) helpRequestEntity.Metadata;
            helpRequestMetadata.Should().BeEquivalentTo("{\"tested_date\": \"02-02-2020\"}");
        }
    }
}
