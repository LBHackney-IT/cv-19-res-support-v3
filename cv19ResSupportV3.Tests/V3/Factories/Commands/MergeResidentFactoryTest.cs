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
        public void CanMapCreateResidentAndHelpRequestToMergeResidentCommand()
        {
            var request = _fixture.Build<CreateResidentAndHelpRequest>().Create();
            var command = request.ToMergeResidentCommand();
            request.Should().BeEquivalentTo(command);
            command.Should().BeOfType<MergeResident>();
        }
    }
}
