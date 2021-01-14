//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Amazon.Lambda.APIGatewayEvents;
//using AutoFixture;
//using cv19ResSupportV3.Tests.V3.Helper;
//using cv19ResSupportV3.V3.Boundary.Response;
//using cv19ResSupportV3.V3.Factories;
//using cv19ResSupportV3.V3.Infrastructure;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using NUnit.Framework;
//
//namespace cv19ResSupportV3.Tests.V3.E2ETests
//{
//    [TestFixture]
//    public class GetLookups : IntegrationTests<Startup>
//    {
//        private Fixture _fixture = new Fixture();
//
//        [SetUp]
//        public void SetUp()
//        {
//            DatabaseContext.Database.RollbackTransaction();
//            E2ETestHelpers.ClearTable(DatabaseContext);
//        }
//
//
//        [Test]
//        public async Task GetLookupsReturnsLookups()
//        {
//            var lookups = _fixture.CreateMany<LookupEntity>().ToList();
//            DatabaseContext.Lookups.AddRange(lookups);
//            DatabaseContext.SaveChanges();
//            var requestUri = new Uri($"api/v3/lookups", UriKind.Relative);
//            var response = Client.GetAsync(requestUri);
//            var statusCode = response.Result.StatusCode;
//            statusCode.Should().Be(200);
//            var responseBody = response.Result.Content;
//            var stringResponse = await responseBody.ReadAsStringAsync().ConfigureAwait(true);
//            var deserializedBody = JsonConvert.DeserializeObject<List<LookupResponse>>(stringResponse);
//            DatabaseContext.Lookups.Count().Should().Be(lookups.Count);
//            deserializedBody.Count.Should().Be(lookups.Count);
//        }
//    }
//}
