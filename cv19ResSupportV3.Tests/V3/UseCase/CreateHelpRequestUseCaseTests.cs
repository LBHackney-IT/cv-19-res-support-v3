using cv19ResRupportV3.V3.Gateways;
using cv19ResRupportV3.V3.UseCase;
using Moq;
using NUnit.Framework;
namespace cv19ResRupportV3.Tests.V1.UseCase
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

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Writing-Unit-Tests
    }
}
