using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories.Commands
{
    [TestFixture]
    public class CreateHelpRequestFactoryTest
    {
        Fixture _fixture = new Fixture();
        [Test]
        public void CanMapCreateHelpRequestCommandToHelpRequestEntity()
        {
            var entity = _fixture.Build<CreateHelpRequest>().Create();
            var command = entity.ToEntity();
            command.Should().BeEquivalentTo(entity);
            command.Should().BeOfType<HelpRequestEntity>();
        }
        [Test]
        public void CanMapHelpRequestCreateRequestBoundaryToCreateResidentAndHelpRequest()
        {
            var request = _fixture.Build<HelpRequestCreateRequestBoundary>().Create();
            var command = request.ToCommand();
            command.Should().BeEquivalentTo(request);
            command.Should().BeOfType<CreateResidentAndHelpRequest>();
        }
    }
}
