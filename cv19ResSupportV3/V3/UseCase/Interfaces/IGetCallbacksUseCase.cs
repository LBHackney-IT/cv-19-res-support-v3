using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetCallbacksUseCase
    {
        List<HelpRequestGetResponse> Execute(CallbackRequestParams requestParams);
    }
}
