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
            var entityObject = EntityHelpers.createHelpRequestEntity(callHandler: new cv19ResSupportV3.V3.Infrastructure.CallHandlerEntity() { Name = "Steve" });
            var domainObject = entityObject.ToDomain();
            entityObject.Should().BeEquivalentTo(domainObject, options =>
            {
                options.Excluding(x => x.CaseNotes);
                options.Excluding(x => x.AssignedTo);
                return options;
            });

            domainObject.AssignedTo.Should().BeEquivalentTo(entityObject.CallHandlerEntity.Name);
        }
    }
}
