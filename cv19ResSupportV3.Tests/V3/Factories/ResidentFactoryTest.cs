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
            var request = new CreateResident();
            var command = request.ToResidentEntity();
            command.Should().BeEquivalentTo(request);
            command.Should().BeOfType<ResidentEntity>();
        }
    }
}
