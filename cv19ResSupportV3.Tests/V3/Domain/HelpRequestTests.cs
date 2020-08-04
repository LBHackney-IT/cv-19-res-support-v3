using System;
using cv19ResSupportV3.V3.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Domain
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
         public void EntitiesHaveAnId()
         {
             var entity = new HelpRequest();
             entity.Id.Should().BeNull();
         }

        [Test]
        public void EntitiesHaveADateTimeRecorded()
        {
            var entity = new HelpRequest();
            var date = new DateTime(2019, 02, 21);
            entity.DateTimeRecorded = date;
            entity.DateTimeRecorded.Should().BeSameDateAs(date);
        }
    }
}
