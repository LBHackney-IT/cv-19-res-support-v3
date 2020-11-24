using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Response;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    public class GetCalls : IntegrationTests<Startup>
    {

        [SetUp]
        public void SetUp()
        {
            CustomizeAssertions.ApproximationDateTime();
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }


        [Test]
        public async Task ReturnsNoCallsIfNoCallsExistForAHelpRequest()
        {
            var helpRequest = EntityHelpers.createHelpRequestEntity();
            helpRequest.CallbackRequired = true;
            helpRequest.InitialCallbackCompleted = true;
            helpRequest.DateTimeRecorded = DateTime.Today.AddDays(-2);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);

            DatabaseContext.SaveChanges();
            var requestUri = new Uri($"api/v3/help-requests/{helpRequest.Id}/calls", UriKind.Relative);
            var response = Client.GetAsync(requestUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var responseBody = response.Result.Content;
            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
            var deserializedBody = JsonConvert.DeserializeObject<List<CallGetResponse>>(stringResponse);
            DatabaseContext.HelpRequestEntities.Find(helpRequest.Id).HelpRequestCalls.Count.Should().Be(0);
            deserializedBody.Count.Should().Be(0);
        }
    }
}
