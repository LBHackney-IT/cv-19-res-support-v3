using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{

    public class PatchCaseNoteUseCaseTests
    {
        private Mock<ICaseNotesGateway> _fakeGateway;
        private PatchCaseNoteUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _fakeGateway = new Mock<ICaseNotesGateway>();
            _classUnderTest = new PatchCaseNoteUseCase(_fakeGateway.Object);
        }

        [Test]
        public void CallsPatchCaseNoteGatewayMethod()
        {
            var id = 12;
            var residentId = 2;
            _fakeGateway.Setup(x => x.PatchCaseNote(id, residentId, "string"))
                .Returns(new ResidentCaseNote());
            _classUnderTest.Execute(id, residentId, "string");
            _fakeGateway.Verify(x => x.PatchCaseNote(id, residentId, "string"), Times.Once());
        }
    }
}
