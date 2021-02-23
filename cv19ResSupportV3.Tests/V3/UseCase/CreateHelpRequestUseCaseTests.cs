using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class CreateHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private CreateHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new CreateHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void SavesHelpRequestIfItDoesNotExist()
        {
            _mockGateway.Setup(s => s.FindHelpRequestByCtasId(It.IsAny<string>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.FindHelpRequestByMetadata(It.IsAny<string>(), It.IsAny<string>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>())).Returns(1);

            var dataToSave = new Fixture().Build<CreateHelpRequest>().Create();
            var response = _classUnderTest.Execute(1, dataToSave);
            _mockGateway.Verify(m => m.FindHelpRequestByCtasId(It.IsAny<string>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("nsss_id", It.IsAny<object>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("spl_id", It.IsAny<object>()), Times.Once());
            _mockGateway.Verify(m => m.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>()), Times.Once());
            response.Should().Be(1);
        }
        [Test]
        public void DoesNotCreateNewHelpRequestIfItDoesExistWithCtasNumber()
        {
            _mockGateway.Setup(s => s.FindHelpRequestByCtasId(It.IsAny<string>())).Returns(1);
            _mockGateway.Setup(s => s.FindHelpRequestByMetadata(It.IsAny<string>(), It.IsAny<object>())).Returns<int?>(null);

            var dataToSave = new Fixture().Build<CreateHelpRequest>().Create();
            var response = _classUnderTest.Execute(1, dataToSave);
            _mockGateway.Verify(m => m.FindHelpRequestByCtasId(It.IsAny<string>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("nsss_id", It.IsAny<object>()), Times.Never());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("spl_id", It.IsAny<object>()), Times.Never());
            _mockGateway.Verify(m => m.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>()), Times.Never);
            response.Should().Be(1);
        }

        [Test]
        public void DoesNotCreateNewHelpRequestIfItExistWithNSSSMetadata()
        {
            _mockGateway.Setup(s => s.FindHelpRequestByCtasId(It.IsAny<string>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.FindHelpRequestByMetadata(It.IsAny<string>(), It.IsAny<object>())).Returns(1);

            var dataToSave = new Fixture().Build<CreateHelpRequest>().Create();
            var response = _classUnderTest.Execute(1, dataToSave);
            _mockGateway.Verify(m => m.FindHelpRequestByCtasId(It.IsAny<string>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("nsss_id", It.IsAny<object>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("spl_id", It.IsAny<object>()), Times.Never());
            _mockGateway.Verify(m => m.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>()), Times.Never);
            response.Should().Be(1);
        }
        [Test]
        public void DoesNotCreateNewHelpRequestIfItExistWithSPLMetadata()
        {
            _mockGateway.Setup(s => s.FindHelpRequestByCtasId(It.IsAny<string>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.FindHelpRequestByMetadata("nsss_id", It.IsAny<string>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.FindHelpRequestByMetadata("spl_id", It.IsAny<object>())).Returns(1);

            var dataToSave = new Fixture().Build<CreateHelpRequest>().Create();
            var response = _classUnderTest.Execute(1, dataToSave);
            _mockGateway.Verify(m => m.FindHelpRequestByCtasId(It.IsAny<string>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("nsss_id", It.IsAny<object>()), Times.Once());
            _mockGateway.Verify(m => m.FindHelpRequestByMetadata("spl_id", It.IsAny<object>()), Times.Once());
            _mockGateway.Verify(m => m.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>()), Times.Never);
            response.Should().Be(1);
        }
    }
}
