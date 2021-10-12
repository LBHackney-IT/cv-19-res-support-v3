using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.E2ETests
{
    [TestFixture]
    public class GetCallHandlersTests : IntegrationTests<Startup>
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseContext.Database.RollbackTransaction();
            E2ETestHelpers.ClearTable(DatabaseContext);
        }

        [Test]
        public async Task GetCallHandlers()
        {
            var callHandler = new Fixture().Build<CallHandlerEntity>()
                .Create();

            DatabaseContext.CallHandlerEntities.Add(callHandler);
            DatabaseContext.SaveChanges();

            var uri = new Uri($"api/v4/call-handlers", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);
            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<CallHandlerResponseBoundary>>(stringContent);
            var callHandlerEntity = DatabaseContext.CallHandlerEntities.FirstOrDefault();

            var responseCallHandler = convertedResponse.First();
            responseCallHandler.Id.Should().Be(callHandlerEntity.Id);
            responseCallHandler.Name.Should().Be(callHandlerEntity.Name);
            responseCallHandler.Email.Should().Be(callHandlerEntity.Email);
        }
    }
}
