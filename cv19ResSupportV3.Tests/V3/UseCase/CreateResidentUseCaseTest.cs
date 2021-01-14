using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class CreateResidentUseCaseTest
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private CreateResidentUseCase _classUnderTest;
        private CreateResident _createResidentCommand;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new CreateResidentUseCase(_mockGateway.Object);

            _createResidentCommand = new Fixture().Build<CreateResident>().Create();

        }

        [Test]
        public void CreatesANewResidentIfItDoesntExist()
        {

            var residentDomain = new Fixture().Build<Resident>()
                .With(x => x.Id, 1)
                .With(x => x.PostCode, _createResidentCommand.PostCode)
                .With(x => x.Uprn, _createResidentCommand.Uprn)
                .With(x => x.Ward, _createResidentCommand.Ward)
                .With(x => x.AddressFirstLine, _createResidentCommand.AddressFirstLine)
                .With(x => x.AddressSecondLine, _createResidentCommand.AddressSecondLine)
                .With(x => x.AddressThirdLine, _createResidentCommand.AddressThirdLine)
                .With(x => x.IsPharmacistAbleToDeliver, _createResidentCommand.IsPharmacistAbleToDeliver)
                .With(x => x.NameAddressPharmacist, _createResidentCommand.NameAddressPharmacist)
                .With(x => x.FirstName, _createResidentCommand.FirstName)
                .With(x => x.LastName, _createResidentCommand.LastName)
                .With(x => x.DobMonth, _createResidentCommand.DobMonth)
                .With(x => x.DobYear, _createResidentCommand.DobYear)
                .With(x => x.DobDay, _createResidentCommand.DobDay)
                .With(x => x.ContactTelephoneNumber, _createResidentCommand.ContactTelephoneNumber)
                .With(x => x.ContactMobileNumber, _createResidentCommand.ContactMobileNumber)
                .With(x => x.EmailAddress, _createResidentCommand.EmailAddress)
                .With(x => x.GpSurgeryDetails, _createResidentCommand.GpSurgeryDetails)
                .With(x => x.NumberOfChildrenUnder18, _createResidentCommand.NumberOfChildrenUnder18)
                .With(x => x.ConsentToShare, _createResidentCommand.ConsentToShare)
                .With(x => x.NhsNumber, _createResidentCommand.NhsNumber).Create();
            _mockGateway.Setup(s => s.FindResident(It.IsAny<FindResident>())).Returns<int?>(null);
            _mockGateway.Setup(s => s.CreateResident(It.IsAny<CreateResident>())).Returns(residentDomain);

            var response = _classUnderTest.Execute(_createResidentCommand);

            _mockGateway.Verify(m => m.FindResident(It.IsAny<FindResident>()), Times.Once());
            _mockGateway.Verify(m => m.CreateResident(It.IsAny<CreateResident>()), Times.Once());
            _mockGateway.Verify(m => m.UpdateResident(It.IsAny<int>(), It.IsAny<UpdateResident>()), Times.Never());
            response.Should().Be(residentDomain);
        }
        [Test]
        public void UpdatesResidentIfItDoesExist()
        {
            _createResidentCommand.PostCode = "something else";

            var updatedResident = new Fixture().Build<Resident>()
                .With(x => x.Id, 1)
                .With(x => x.PostCode, "something else")
                .With(x => x.Uprn, _createResidentCommand.Uprn)
                .With(x => x.Ward, _createResidentCommand.Ward)
                .With(x => x.AddressFirstLine, _createResidentCommand.AddressFirstLine)
                .With(x => x.AddressSecondLine, _createResidentCommand.AddressSecondLine)
                .With(x => x.AddressThirdLine, _createResidentCommand.AddressThirdLine)
                .With(x => x.IsPharmacistAbleToDeliver, _createResidentCommand.IsPharmacistAbleToDeliver)
                .With(x => x.NameAddressPharmacist, _createResidentCommand.NameAddressPharmacist)
                .With(x => x.FirstName, _createResidentCommand.FirstName)
                .With(x => x.LastName, _createResidentCommand.LastName)
                .With(x => x.DobMonth, _createResidentCommand.DobMonth)
                .With(x => x.DobYear, _createResidentCommand.DobYear)
                .With(x => x.DobDay, _createResidentCommand.DobDay)
                .With(x => x.ContactTelephoneNumber, _createResidentCommand.ContactTelephoneNumber)
                .With(x => x.ContactMobileNumber, _createResidentCommand.ContactMobileNumber)
                .With(x => x.EmailAddress, _createResidentCommand.EmailAddress)
                .With(x => x.GpSurgeryDetails, _createResidentCommand.GpSurgeryDetails)
                .With(x => x.NumberOfChildrenUnder18, _createResidentCommand.NumberOfChildrenUnder18)
                .With(x => x.ConsentToShare, _createResidentCommand.ConsentToShare)
                .With(x => x.NhsNumber, _createResidentCommand.NhsNumber).Create();


            _mockGateway.Setup(s => s.FindResident(It.IsAny<FindResident>())).Returns(1);
            _mockGateway.Setup(s => s.UpdateResident(It.IsAny<int>(), It.IsAny<UpdateResident>())).Returns(updatedResident);

            var response = _classUnderTest.Execute(_createResidentCommand);

            _mockGateway.Verify(m => m.FindResident(It.IsAny<FindResident>()), Times.Once());
            _mockGateway.Verify(m => m.CreateResident(It.IsAny<CreateResident>()), Times.Never());
            _mockGateway.Verify(m => m.UpdateResident(It.IsAny<int>(), It.IsAny<UpdateResident>()), Times.Once());
            response.Should().Be(updatedResident);
        }
    }
}
