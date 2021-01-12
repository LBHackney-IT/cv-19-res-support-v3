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
            var createdEntity = DatabaseContext.HelpRequestEntities.Find(convertedResponse.Id);
            createdEntity.Uprn.Should().BeEquivalentTo(requestObject.Uprn);
            createdEntity.FirstName.Should().BeEquivalentTo(requestObject.FirstName);
            createdEntity.LastName.Should().BeEquivalentTo(requestObject.LastName);
            createdEntity.RecordStatus.Should().Be("MASTER");
            createdEntity.HelpWithAccessingSupermarketFood.Should().Be(requestObject.HelpWithAccessingSupermarketFood);
            createdEntity.HelpWithCompletingNssForm.Should().Be(requestObject.HelpWithCompletingNssForm);
            createdEntity.HelpWithShieldingGuidance.Should().Be(requestObject.HelpWithShieldingGuidance);
            createdEntity.HelpWithNoNeedsIdentified.Should().Be(requestObject.HelpWithNoNeedsIdentified);
            DatabaseContext.Database.BeginTransaction();
        }
    }
}
