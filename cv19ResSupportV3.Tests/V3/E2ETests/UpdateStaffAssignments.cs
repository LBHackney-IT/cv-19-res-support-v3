using System;
using System.Collections.Generic;
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
    public class UpdateStaffAssignments : IntegrationTests<Startup>
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }
        [Test]
        public void UpdateStaffAssignmentAddsAssignedToRecord()
        {
            var residentEntity = DatabaseContext.ResidentEntities.Add(EntityHelpers.createResident());
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(123, residentEntity.Entity.Id));
            helpRequestEntity.Entity.AssignedTo = null;
            helpRequestEntity.Entity.HelpNeeded = "Help Request";
            helpRequestEntity.Entity.CallbackRequired = true;
            helpRequestEntity.Entity.InitialCallbackCompleted = false;
            DatabaseContext.SaveChanges();
            var request = new UpdateStaffAssignmentsRequestBoundary
            {
                HelpNeeded = "Help Request",
                StaffMembers = new List<string> {"J J", "M M"}
            };
            var data = JsonConvert.SerializeObject(request);
            HttpContent postContent = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/v3/help-requests/staff-assignments", UriKind.Relative);
            var response = Client.PostAsync(uri, postContent);
            postContent.Dispose();
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
        }
    }
}
