using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    public class UpdateHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _fakeGateway;
        private UpdateHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _fakeGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new UpdateHelpRequestUseCase(_fakeGateway.Object);
        }

        [Test]
        public void CallsPatchCaseNoteGatewayMethodWithCorrectValues()
        {
            var id = 12;
            var command = new UpdateHelpRequest();
            _fakeGateway.Setup(x => x.UpdateHelpRequest(id, command))
                .Returns(new HelpRequest());
            _classUnderTest.Execute(id, command);
            _fakeGateway.Verify(x => x.UpdateHelpRequest(id, command), Times.Once());
        }
    }
}
