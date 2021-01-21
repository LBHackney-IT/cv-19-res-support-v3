using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using NUnit.Framework;
using cv19ResSupportV3.V3.Factories;
using FluentAssertions;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class ResponseFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapADomainToAResponseObject()
        {
            var entityObject = _fixture.Build<HelpRequest>().Create();
            var responseObject = entityObject.ToResponse();
            responseObject.Should().BeEquivalentTo(entityObject, options =>
            {
                options.Excluding(x => x.CaseNotes);
                return options;
            });
        }
    }
}
