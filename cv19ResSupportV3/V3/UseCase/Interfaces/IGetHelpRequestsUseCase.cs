using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetHelpRequestsUseCase
    {
        List<HelpRequestGetResponse> Execute(RequestQueryParams queryParams);
    }
}
