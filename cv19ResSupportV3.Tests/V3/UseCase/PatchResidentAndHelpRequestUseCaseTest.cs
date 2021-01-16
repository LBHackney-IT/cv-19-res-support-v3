using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    public class PatchResidentAndHelpRequestUseCaseTest
    {
        private Mock<IPatchResidentUseCase> _fakePatchResidentUseCase;
        private Mock<IPatchHelpRequestUseCase> _fakePatchHelpRequestUseCase;
        private PatchResidentAndHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _fakePatchResidentUseCase = new Mock<IPatchResidentUseCase>();
            _fakePatchHelpRequestUseCase = new Mock<IPatchHelpRequestUseCase>();
            _classUnderTest = new PatchResidentAndHelpRequestUseCase(_fakePatchResidentUseCase.Object,
                _fakePatchHelpRequestUseCase.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var id = 12;
            var residentId = 2;
            var patchHelpRequestFull = new PatchResidentAndHelpRequest() { FirstName = "Jay", HelpNeeded = "something" };
            var response = new HelpRequest { HelpNeeded = "something", ResidentId = residentId };
            _fakePatchHelpRequestUseCase.Setup(x => x.Execute(It.Is<int>(x => x == id), It.IsAny<PatchHelpRequest>()))
                .Returns(response);
            _fakePatchResidentUseCase.Setup(x => x.Execute(It.IsAny<int>(), It.IsAny<PatchResident>()))
                .Returns(new Resident());
            _classUnderTest.Execute(id, patchHelpRequestFull);
            _fakePatchHelpRequestUseCase.Verify(
                x => x.Execute(id, It.Is<PatchHelpRequest>(x => x.HelpNeeded == "something")),
                Times.Once());
            _fakePatchResidentUseCase.Verify(x => x.Execute(residentId, It.Is<PatchResident>(x => x.FirstName == "Jay")), Times.Once());
        }
    }
}
