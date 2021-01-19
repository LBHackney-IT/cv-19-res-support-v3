using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Factories;
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
            var entityObject = EntityHelpers.createHelpRequestEntity();
            var domainObject = entityObject.ToDomain();
            entityObject.Should().BeEquivalentTo(domainObject, options =>
            {
                options.Excluding(x => x.CaseNotes);
                return options;
            });
        }
    }
}
