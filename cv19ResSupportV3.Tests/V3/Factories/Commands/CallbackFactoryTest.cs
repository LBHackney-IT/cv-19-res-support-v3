using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4.UseCase.Enumeration;
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
            var requestObject = new CallbackRequestParams() { HelpNeeded = "Help" };
            var command = requestObject.ToCommand();
            command.Should().BeEquivalentTo(new CallbackQuery() { HelpNeeded = "Help", ExcludedHelpTypes = HelpTypes.Excluded });
        }

        [Test]
        public void MapsExcludedTypes()
        {
            var requestObject = new CallbackRequestParams() { HelpNeeded = "Help", IncludeType = "EUSS" };
            var command = requestObject.ToCommand();
            command.Should().BeEquivalentTo(new CallbackQuery() { HelpNeeded = "Help", ExcludedHelpTypes = new List<string>() });
        }
    }
}
