using System;
using cv19ResSupportV3.V3.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Domain
{
    [TestFixture]
    public class HelpRequestCallTest
    {
        [Test]
        public void helpReuestCallHasAnId()
        {
            var helpRequestCall = new HelpRequestCall();
            helpRequestCall.Id.Should().Be(0);
        }


    }
}
