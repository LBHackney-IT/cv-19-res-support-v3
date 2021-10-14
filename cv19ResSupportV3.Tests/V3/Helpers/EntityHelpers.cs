using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Infrastructure;
using LBHFSSPublicAPI.Tests.TestHelpers;
using AutoFixture;

namespace cv19ResSupportV3.Tests.V3.Helpers
{
    public static class EntityHelpers
    {
        public static ResidentEntity createResident(int id = 1)
        {
            var helpRequestEntity = Randomm.Build<ResidentEntity>()
                .With(x => x.Id, id)
                .Without(h => h.CaseNotes)
                .Without(h => h.HelpRequests)
                .Create();
            return helpRequestEntity;
        }

        public static HelpRequestEntity createHelpRequestEntity(int id = 1, int residentId = 1, string helpNeeded = "CallType", string helpNeededSubtype = "Repairs", CallHandlerEntity callHandler = null)
        {
            var helpRequestEntity = Randomm.Build<HelpRequestEntity>()
                .With(x => x.Id, id)
                .With(x => x.ResidentId, residentId)
                .With(x => x.HelpNeeded, helpNeeded)
                .With(x => x.HelpNeededSubtype, helpNeededSubtype)
                .With(x => x.CallHandlerEntity, callHandler)
                .Without(x => x.CallHandlerId)
                .Without(h => h.HelpRequestCalls)
                .Without(h => h.CaseNotes)
                .Without(h => h.ResidentEntity)
                .Create();
            return helpRequestEntity;
        }

        public static List<HelpRequestEntity> createHelpRequestEntities(int count = 3, int residentId = 1)
        {
            var helpRequestEntities = Randomm.Build<HelpRequestEntity>()
                .Without(h => h.HelpRequestCalls)
                .Without(h => h.CaseNotes)
                .Without(h => h.ResidentEntity)
                .Without(h => h.CallHandlerEntity)
                .Without(h => h.CallHandlerId)
                .With(x => x.ResidentId, residentId)
                .CreateMany(count)
                .ToList();
            return helpRequestEntities;
        }

        public static HelpRequestCallEntity createHelpRequestCallEntity()
        {
            return Randomm.Build<HelpRequestCallEntity>()
                .Without(h => h.HelpRequestEntity)
                .Create();
        }

        public static List<HelpRequestCallEntity> createHelpRequestCallEntities(int count = 3)
        {
            var calls = Randomm.Build<HelpRequestCallEntity>()
                .Without(h => h.HelpRequestEntity).CreateMany(count).ToList();
            return calls;
        }
    }
}
