using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class CreateResidentFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapCreateResidentAndHelpRequestToCreateResidentCommand()
        {
            var request = _fixture.Build<CreateResidentAndHelpRequest>().Create();
            var command = request.ToCreateResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<CreateResident>();
        }
    }
}
