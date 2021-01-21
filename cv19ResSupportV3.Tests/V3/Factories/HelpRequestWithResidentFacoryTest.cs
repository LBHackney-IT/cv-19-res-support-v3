using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Factories
{
    public class HelpRequestWithResidentFacoryTest
    {
        [Test]
        public void CanMapHelpRequestWithResidentEntitiesToDomain()
        {
            var residentId = 143;
            var resident = Randomm.Build<ResidentEntity>()
                .Without(h => h.CaseNotes)
                .With(x => x.Id, residentId)
                .Without(h => h.HelpRequests)
                .Create();
            var request = Randomm.Build<HelpRequestEntity>()
                .With(x => x.Id)
                .With(x => x.ResidentId, residentId)
                .Without(h => h.HelpRequestCalls)
                .Without(h => h.CaseNotes)
                .With(h => h.ResidentEntity, resident)
                .Create();
            var domain = request.ToHelpRequestWithResidentDomain();
            domain.Should().BeEquivalentTo(resident.ToDomain(request));
            domain.Should().BeOfType<HelpRequestWithResident>();
        }
    }
}
