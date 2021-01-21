using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class HelpRequestCallControllerTests
    {
        private HelpRequestCallController _classUnderTest;
        private Mock<ICreateHelpRequestCallUseCase> _fakeCreateHelpRequestCallUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestCallUseCase = new Mock<ICreateHelpRequestCallUseCase>();
            _classUnderTest = new HelpRequestCallController(_fakeCreateHelpRequestCallUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var helpRequest = new Fixture().Build<HelpRequest>()
                .Create();
            var request = new Fixture().Create<CreateHelpRequestCallRequest>();
            _fakeCreateHelpRequestCallUseCase.Setup(x => x.Execute(helpRequest.Id, It.IsAny<CreateHelpRequestCall>()))
                .Returns(1);
            var response = _classUnderTest.CreateHelpRequestCall(helpRequest.Id, request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }
    }
}
