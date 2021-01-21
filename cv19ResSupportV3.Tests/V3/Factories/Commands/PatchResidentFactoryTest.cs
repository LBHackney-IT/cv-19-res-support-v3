using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    public class PatchResidentFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapCreateResidentCommandToPatchResidentCommand()
        {
            var request = _fixture.Build<CreateResident>().Create();
            var command = request.ToPatchResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<PatchResident>();
        }
    }
}
