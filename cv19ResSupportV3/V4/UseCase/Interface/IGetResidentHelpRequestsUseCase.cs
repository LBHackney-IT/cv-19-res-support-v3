using System.Collections.Generic;
using cv19ResSupportV3.V4.Boundary.Response;

namespace cv19ResSupportV3.V4.UseCase.Interface
{
    public interface IGetResidentHelpRequestsUseCase
    {
        List<ResidentHelpRequestResponse> Execute(int id, IEnumerable<string> excludedHelpTypes);
    }
}
