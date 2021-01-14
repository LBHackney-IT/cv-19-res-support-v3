using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class CreateHelpRequest : IntegrationTests<Startup>
    {
        [Test]
        public async Task GetResidentInformationByIdReturnsTheCorrectInformation()
        {
            DatabaseContext.Database.RollbackTransaction();
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
            DatabaseContext.Database.BeginTransaction();
        }
    }
}
