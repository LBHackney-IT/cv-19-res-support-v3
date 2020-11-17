using System;
using AutoFixture;
using NUnit.Framework;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    [TestFixture]
    public class ResponseFactoryTest
    {
        Fixture _fixture = new Fixture();

        [Test]
        public void CanMapAEntityToAResponseObject()
        {
            var entityObject = _fixture.Build<HelpRequestEntity>().Create();
            var responseObject = entityObject.ToResponse();
            responseObject.Should().BeEquivalentTo(entityObject);
        }

        [Test]
        public void CanMapListOfEntityObjectsToListOfResponseObjects()
        {
            var entityObjects = _fixture.CreateMany<HelpRequestEntity>();
            var responseObjects = entityObjects.ToResponse();
            responseObjects.Should().BeEquivalentTo(entityObjects);
        }
    }
}
