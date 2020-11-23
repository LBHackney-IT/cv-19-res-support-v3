using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class HelpRequestFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Build<HelpRequestEntity>().Create();
            var entity = databaseEntity.ToDomain();
            databaseEntity.Should().BeEquivalentTo(entity);
        }

        [Test]
        public void CanMapADatabaseDomainToAnEntityObject()
        {
            var domainObject = _fixture.Build<HelpRequest>().Create();
            var entityObject = domainObject.ToEntity();
            entityObject.Should().BeEquivalentTo(domainObject);
        }
    }
}
