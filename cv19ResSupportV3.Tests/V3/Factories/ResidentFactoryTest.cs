using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class ResidentFactoryTest
    {
        private Fixture _fixture = new Fixture();

        [Test]
        public void CanMapResidentEntitiesResidentDomain()
        {
            var request = new ResidentEntity();
            var command = request.ToResidentDomain();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<Resident>();
        }
        [Test]
        public void CanMapCreateResidentDomainToResidentEntity()
        {
            var request = _fixture.Create<CreateResident>();
            var command = request.ToResidentEntity();

            command.Should().BeOfType<ResidentEntity>();

            command.Postcode.Should().Be(request.Postcode);
            command.Uprn.Should().Be(request.Uprn);
            command.Ward.Should().Be(request.Ward);
            command.AddressFirstLine.Should().Be(request.AddressFirstLine);
            command.AddressSecondLine.Should().Be(request.AddressSecondLine);
            command.AddressThirdLine.Should().Be(request.AddressThirdLine);
            command.IsPharmacistAbleToDeliver.Should().Be(request.IsPharmacistAbleToDeliver);
            command.NameAddressPharmacist.Should().Be(request.NameAddressPharmacist);
            command.FirstName.Should().Be(request.FirstName);
            command.LastName.Should().Be(request.LastName);
            command.DobMonth.Should().Be(request.DobMonth);
            command.DobYear.Should().Be(request.DobYear);
            command.DobDay.Should().Be(request.DobDay);
            command.ContactTelephoneNumber.Should().Be(request.ContactTelephoneNumber);
            command.ContactMobileNumber.Should().Be(request.ContactMobileNumber);
            command.EmailAddress.Should().Be(request.EmailAddress);
            command.GpSurgeryDetails.Should().Be(request.GpSurgeryDetails);
            command.NumberOfChildrenUnder18.Should().Be(request.NumberOfChildrenUnder18);
            command.ConsentToShare.Should().Be(request.ConsentToShare);
            command.NhsNumber.Should().Be(request.NhsNumber);
        }
    }
}
