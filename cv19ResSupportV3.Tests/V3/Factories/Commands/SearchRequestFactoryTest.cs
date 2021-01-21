using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class SearchRequestFactoryTest
    {
        Fixture _fixture = new Fixture();

        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var request = _fixture.Build<RequestQueryParams>().Create();
            var command = request.ToCommand();

            command.Should().BeOfType<SearchRequest>();
            command.Should().BeEquivalentTo(request);
        }
    }
}
