using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class CreateHelpRequestCallFactory
    {
        public static CreateHelpRequestCall ToCommand(this CreateHelpRequestCallRequest request)
        {
            return new CreateHelpRequestCall()
            {
                HelpRequestId = request.HelpRequestId,
                CallType = request.CallType,
                CallDirection = request.CallDirection,
                CallOutcome = request.CallOutcome,
                CallDateTime = request.CallDateTime,
                CallHandler = request.CallHandler
            };
        }

        public static HelpRequestCallEntity ToEntity(this CreateHelpRequestCall request)
        {
            return new HelpRequestCallEntity()
            {
                HelpRequestId = request.HelpRequestId,
                CallType = request.CallType,
                CallDirection = request.CallDirection,
                CallOutcome = request.CallOutcome,
                CallDateTime = request.CallDateTime,
                CallHandler = request.CallHandler
            };
        }
    }
}
