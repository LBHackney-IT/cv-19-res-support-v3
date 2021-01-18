using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class UpdateHelpRequestFactoryTest
    {
        Fixture _fixture = new Fixture();

        [Test]
        public void CanMapHelpRequestPatchRequestToUpdateHelpRequestCommand()
        {
            var request = _fixture.Build<HelpRequestUpdateRequest>().Create();
            var command = request.ToCommand();
            command.Should().BeEquivalentTo(request);
            command.Should().BeOfType<UpdateResidentAndHelpRequest>();
        }

        [Test]
        public void CanMapUpdateResidentAndHelpRequestToUpdateHelpRequestCommand()
        {
            var request = _fixture.Build<UpdateResidentAndHelpRequest>().Create();
            var command = request.ToUpdateHelpRequestCommand();
            command.Should().BeEquivalentTo(request);
            command.Should().BeOfType<UpdateResidentAndHelpRequest>();
        }
    }
}
