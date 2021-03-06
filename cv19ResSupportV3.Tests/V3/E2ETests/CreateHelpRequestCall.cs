using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    [TestFixture]
    public class CreateHelpRequestCall : IntegrationTests<Startup>
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }
        [Test]
        public void CreateHelpRequestCallReturnsTheCorrectInformation()
        {
            var resident = EntityHelpers.createResident(2);
            var helpRequestEntity = EntityHelpers.createHelpRequestEntity(1, resident.Id);
            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequestEntity);
            DatabaseContext.SaveChanges();
            var requestObject = new Fixture().Create<CreateHelpRequestCallRequest>();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/1/calls", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(201);
            var createdEntity = DatabaseContext.HelpRequestCallEntities.OrderByDescending(x => x.Id).FirstOrDefault();
            createdEntity.HelpRequestId.Should().Be(1);
            createdEntity.CallType.Should().Be(requestObject.CallType);
            createdEntity.CallDirection.Should().Be(requestObject.CallDirection);
            createdEntity.CallOutcome.Should().Be(requestObject.CallOutcome);
            createdEntity.CallDateTime.Should().BeCloseTo(requestObject.CallDateTime, 2000);
        }

        [Test]
        public void AttemptToCreateCallOnNonExistentHelpRequestReturnsNotFound()
        {
            var requestObject = EntityHelpers.createHelpRequestEntity();
            var data = JsonConvert.SerializeObject(requestObject);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/1/calls", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }

    }
}


