using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    public class UpdateHelpRequestFactoryTest
    {
        [Test]
        public void CanMapUpdateHelpRequestCommandToHelpRequestEntity()
        {
            var entity = new UpdateHelpRequest();
            var command = entity.ToEntity();
            command.Should().BeOfType<HelpRequestEntity>();
        }
        [Test]
        public void CanMapHelpRequestPatchRequestToPatchHelpRequestCommand()
        {
            var request = new HelpRequestUpdateRequest();
            var command = request.ToCommand();
            command.Should().BeOfType<UpdateHelpRequest>();
        }
    }
}
