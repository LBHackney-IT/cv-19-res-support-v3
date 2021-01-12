using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class CallbackFactory
    {
        [Test]
        public void CanMapACallbackRequestToACommand()
        {
            var requestObject = new CallbackRequestParams() { HelpNeeded = "Help"};
            var command = requestObject.ToCommand();
            command.Should().BeEquivalentTo(requestObject);
        }
    }
}
