using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class HelpRequestFactoryTest
    {
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = new HelpRequestEntity();
            var entity = databaseEntity.ToDomain();
            databaseEntity.Should().BeEquivalentTo(entity);
        }
    }
}
