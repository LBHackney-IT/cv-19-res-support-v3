//using System.Collections.Generic;
//using System.Linq;
//using cv19ResSupportV3.V3.Infrastructure;
//using LBHFSSPublicAPI.Tests.TestHelpers;
//using AutoFixture;
//
//namespace cv19ResSupportV3.Tests.V3.Helpers
//{
//    public static class EntityHelpers
//    {
//        public static HelpRequestEntityOld createHelpRequestEntity(int id = 1)
//        {
//            var helpRequestEntity = Randomm.Build<HelpRequestEntityOld>()
//                .With(x => x.Id, id)
//                .Without(h => h.HelpRequestCalls)
//                .Create();
//            return helpRequestEntity;
//        }
//
//        public static List<HelpRequestEntityOld> createHelpRequestEntities(int count = 3)
//        {
//            var helpRequestEntities = Randomm.Build<HelpRequestEntityOld>()
//                .Without(h => h.HelpRequestCalls)
//                .CreateMany(count)
//                .ToList();
//            return helpRequestEntities;
//        }
//
//        public static HelpRequestCallEntity createHelpRequestCallEntity()
//        {
//            return Randomm.Build<HelpRequestCallEntity>()
//                .Without(h => h.HelpRequestEntity)
//                .Create();
//        }
//
//        public static List<HelpRequestCallEntity> createHelpRequestCallEntities(int count = 3)
//        {
//            var calls = Randomm.Build<HelpRequestCallEntity>()
//                .Without(h => h.HelpRequestEntity).CreateMany(count).ToList();
//            return calls;
//        }
//    }
//}
