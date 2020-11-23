using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Infrastructure;
using LBHFSSPublicAPI.Tests.TestHelpers;
using AutoFixture;

namespace cv19ResSupportV3.Tests.V3.Helpers
{
    public static class EntityHelpers
    {
        public static HelpRequestEntity createHelpRequestEntity()
        {
            var helpRequestEntity = Randomm.Build<HelpRequestEntity>()
                .Without(h => h.HelpRequestCalls)
                .Create();
            return helpRequestEntity;
        }

        public static List<HelpRequestEntity> createHelpRequestEntities(int count = 3)
        {
            var helpRequestEntities = Randomm.Build<HelpRequestEntity>()
                .Without(h => h.HelpRequestCalls)
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
    }
}