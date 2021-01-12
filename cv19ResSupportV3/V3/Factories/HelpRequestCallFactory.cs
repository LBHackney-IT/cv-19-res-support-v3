using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class HelpRequestCallFactory
    {
        public static HelpRequestCallEntity ToEntity(this HelpRequestCall helpRequestCall)
        {
            return new HelpRequestCallEntity()
            {
                Id = helpRequestCall.Id,
                HelpRequestId = helpRequestCall.HelpRequestId,
                CallType = helpRequestCall.CallType,
                CallDirection = helpRequestCall.CallDirection,
                CallOutcome = helpRequestCall.CallOutcome,
                CallDateTime = helpRequestCall.CallDateTime,
                CallHandler = helpRequestCall.CallHandler
            };
        }

        public static HelpRequestCall ToDomain(this HelpRequestCallEntity helpRequestCallEntity)
        {
            return new HelpRequestCall()
            {
                Id = helpRequestCallEntity.Id,
                HelpRequestId = helpRequestCallEntity.HelpRequestId,
                CallType = helpRequestCallEntity.CallType,
                CallDirection = helpRequestCallEntity.CallDirection,
                CallOutcome = helpRequestCallEntity.CallOutcome,
                CallDateTime = helpRequestCallEntity.CallDateTime,
                CallHandler = helpRequestCallEntity.CallHandler
            };
        }

        public static List<HelpRequestCall> ToDomain(this ICollection<HelpRequestCallEntity> helpRequestCallEntityList)
        {
            return helpRequestCallEntityList?.Select(hrItem => hrItem.ToDomain()).ToList();
        }
    }
}
