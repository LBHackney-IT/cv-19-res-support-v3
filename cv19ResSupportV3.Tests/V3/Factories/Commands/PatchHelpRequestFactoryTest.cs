using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    public class PatchHelpRequestFactoryTest
    {
        [Test]
        public void CanMapPatchHelpRequestCommandToHelpRequestEntity()
        {
            var entity = new PatchHelpRequest();
            var command = entity.ToEntity();
            command.Should().BeOfType<HelpRequestEntity>();
        }
        [Test]
        public void CanMapHelpRequestPatchRequestToPatchHelpRequestCommand()
        {
            var request = new HelpRequestPatchRequest();
            var command = request.ToCommand();
            command.Should().BeOfType<PatchHelpRequest>();
        }
    }
}
