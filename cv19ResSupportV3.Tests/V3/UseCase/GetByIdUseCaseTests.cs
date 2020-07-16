using cv19ResRupportV3.V1.Gateways;
using cv19ResRupportV3.V1.UseCase;
using Moq;
using NUnit.Framework;

namespace cv19ResRupportV3.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IExampleGateway> _mockGateway;
        private GetByIdUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IExampleGateway>();
            _classUnderTest = new GetByIdUseCase(_mockGateway.Object);
        }

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Writing-Unit-Tests
    }
}
