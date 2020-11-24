using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Response;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface IGetHelpRequestCallsUseCase
    {
        List<CallGetResponse> Execute(int id);
    }
}
