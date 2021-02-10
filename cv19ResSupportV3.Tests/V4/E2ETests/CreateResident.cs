using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class CreateResident : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task CreateResidentRequestCreatesRecord()
        {
            var requestObject = new Fixture().Create<ResidentRequestBoundary>();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/residents", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponseBoundary>(stringContent);
            var residentEntity = DatabaseContext.ResidentEntities.FirstOrDefault();
            convertedResponse.Should().BeEquivalentTo(residentEntity.ToResidentDomain().ToResponse());
        }

        [Test]
        public void CreateResidentRequestWithInvalidRequestObjectReturnsBadRequest()
        {
            ResidentRequestBoundary requestObject = null;
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v4/residents", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(400);
        }
    }
}
