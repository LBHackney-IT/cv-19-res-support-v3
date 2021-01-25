using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCase
{
    public class PatchResidentUseCaseTest
    {
        private Mock<IResidentGateway> _mockResidentGateway;
        private PatchResidentUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockResidentGateway = new Mock<IResidentGateway>();
            _classUnderTest = new PatchResidentUseCase(_mockResidentGateway.Object);
        }

        [Test]
        public void PatchesResident()
        {
            var patchResident = new ResidentRequestBoundary { FirstName = "Jay", LastName = "something"};
            _mockResidentGateway.Setup(gw => gw.PatchResident(It.IsAny<int>(), It.IsAny<PatchResident>()))
                .Returns(patchResident.ToResident());
            var id = Randomm.Id();
            _classUnderTest.Execute(id, patchResident);
            _mockResidentGateway.Verify(gw => gw.PatchResident(It.IsAny<int>(), It.IsAny<PatchResident>()), Times.Once);
        }
    }
}
