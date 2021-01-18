using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class MergeResidentFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapCreateResidentAndHelpRequestToUpdateResidentCommand()
        {
            var request = _fixture.Build<CreateResidentAndHelpRequest>().Create();
            var command = request.ToUpdateResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<UpdateResident>();
        }
        [Test]
        public void CanMapCreateResidentToUpdateResidentCommand()
        {
            var request = _fixture.Build<CreateResident>().Create();
            var command = request.ToUpdateResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<UpdateResident>();
        }

        [Test]
        public void CanMapUpdateResidentAndHelpRequestToUpdateResident()
        {
            var request = _fixture.Build<UpdateResidentAndHelpRequest>().Create();
            var command = request.ToUpdateResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<UpdateResident>();
        }
    }
}
