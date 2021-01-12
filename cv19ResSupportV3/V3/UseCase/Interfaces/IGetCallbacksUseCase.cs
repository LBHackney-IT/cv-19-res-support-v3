using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetCallbacksUseCase
    {
        List<HelpRequest> Execute(CallbackRequestParams requestParams);
    }
}
