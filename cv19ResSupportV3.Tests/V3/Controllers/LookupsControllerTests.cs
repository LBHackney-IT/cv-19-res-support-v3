using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class LookupsControllerTests
    {
        private LookupsController _classUnderTest;
        private Mock<IGetLookupsUseCase> _getLookupsUseCase;

        [SetUp]
        public void SetUp()
        {
            _getLookupsUseCase = new Mock<IGetLookupsUseCase>();
            _classUnderTest = new LookupsController(_getLookupsUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var lookups = new Fixture().CreateMany<LookupDomain>().ToList();
            _getLookupsUseCase.Setup(x => x.Execute(It.IsAny<LookupQuery>()))
                .Returns(lookups);
            var response = _classUnderTest.GetLookups(new LookupQueryParams() { LookupGroup = "one" }) as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }
    }
}
