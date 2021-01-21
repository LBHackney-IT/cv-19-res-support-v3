using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class FindResidentFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapCreateResidenToFindResidentCommand()
        {
            var request = _fixture.Build<CreateResident>().Create();
            var command = request.ToFindResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<FindResident>();
        }
    }
}
