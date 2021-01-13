using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class CreateHelpRequestCallFactoryTest
    {

        [Test]
        public void RequestToCommand()
        {
            var request = new Fixture().Create<CreateHelpRequestCallRequest>();

            var command = request.ToCommand();

            command.HelpRequestId.Should().Be(request.HelpRequestId);
        }

        [Test]
        public void CommandToEntity()
        {
            var command = new Fixture().Create<CreateHelpRequestCall>();

            var entity = command.ToEntity();

            entity.HelpRequestId.Should().Be(command.HelpRequestId);
        }

    }
}
