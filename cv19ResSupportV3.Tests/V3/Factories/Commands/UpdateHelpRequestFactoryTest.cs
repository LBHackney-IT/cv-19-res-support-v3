using AutoFixture;
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
        Fixture _fixture = new Fixture();

        [Test]
        public void CanMapHelpRequestPatchRequestToPatchHelpRequestCommand()
        {
            var request = _fixture.Build<HelpRequestUpdateRequest>().Create();
            var command = request.ToCommand();
            command.Should().BeEquivalentTo(request);
            command.Should().BeOfType<UpdateHelpRequest>();
        }
    }
}
