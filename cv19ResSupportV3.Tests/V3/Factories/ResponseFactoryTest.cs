using cv19ResSupportV3.Tests.V3.Helpers;
using NUnit.Framework;
using cv19ResSupportV3.V3.Factories;
using FluentAssertions;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class ResponseFactoryTest
    {
        [Test]
        public void CanMapAEntityToAResponseObject()
        {
            var entityObject = EntityHelpers.createHelpRequestEntity();
            var responseObject = entityObject.ToResponse();
            responseObject.Should().BeEquivalentTo(entityObject, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
        }

        [Test]
        public void CanMapListOfEntityObjectsToListOfResponseObjects()
        {
            var entityObjects = EntityHelpers.createHelpRequestEntities();
            var responseObjects = entityObjects.ToResponse();
            responseObjects.Should().BeEquivalentTo(entityObjects, options =>
            {
                options.Excluding(ex => ex.HelpRequestCalls);
                return options;
            });
        }
    }
}
