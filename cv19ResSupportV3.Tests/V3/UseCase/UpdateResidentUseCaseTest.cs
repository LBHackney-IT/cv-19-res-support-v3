using System.Runtime.InteropServices;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    public class UpdateResidentUseCaseTest
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private UpdateResidentUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new UpdateResidentUseCase(_mockGateway.Object);

        }
        [Test]
        public void CanUpdateAResident()
        {
            var resident = new Resident();
            _mockGateway.Setup(s => s.UpdateResident(It.IsAny<int>(), It.IsAny<UpdateResident>())).Returns(resident);

            var response = _classUnderTest.Execute(resident.Id, new UpdateResident());

            _mockGateway.Verify(m => m.UpdateResident(It.IsAny<int>(), It.IsAny<UpdateResident>()), Times.Once());
            response.Should().Be(resident);
        }

    }
}
