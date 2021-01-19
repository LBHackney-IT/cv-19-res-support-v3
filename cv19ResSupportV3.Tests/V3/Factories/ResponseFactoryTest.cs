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
        public void CanMapADomainToAResponseObject()
        {
            var entityObject = EntityHelpers.createHelpRequestEntity().ToDomain();
            var responseObject = entityObject.ToResponse();
            responseObject.Should().BeEquivalentTo(entityObject, options =>
            {
                return options;
            });
        }
    }
}
