using AutoFixture;
using cv19ResSupportV3.V4;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.Boundary.Requests
{
    [TestFixture]
    public class ResidentRequestBoundaryTests
    {
        [Test]
        public void ResidentRequestBoundaryShouldHaveTheCorrectProperties()
        {
            var entityType = typeof(ResidentRequestBoundary);
            entityType.GetProperties().Length.Should().Be(21);
            var entity = new Fixture().Create<ResidentRequestBoundary>();
            Assert.That(entity, Has.Property("FirstName").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("LastName").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("DobDay").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("DobMonth").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("DobYear").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("ContactTelephoneNumber").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("ContactMobileNumber").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("EmailAddress").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("AddressFirstLine").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("AddressSecondLine").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("AddressThirdLine").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Postcode").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("Ward").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("IsPharmacistAbleToDeliver").InstanceOf(typeof(bool?)));
            Assert.That(entity, Has.Property("NameAddressPharmacist").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("GpSurgeryDetails").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("NumberOfChildrenUnder18").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("ConsentToShare").InstanceOf(typeof(bool?)));
            Assert.That(entity, Has.Property("RecordStatus").InstanceOf(typeof(string)));
            Assert.That(entity, Has.Property("NhsNumber").InstanceOf(typeof(string)));
        }
    }
}
