using System.Collections.Generic;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Response;

namespace cv19ResSupportV3.V4.UseCase.Interface
{
    public interface IPatchResidentHelpRequestUseCase
    {
        ResidentHelpRequestResponse Execute(int id, int helpRequestId, ResidentHelpRequestRequest request);
    }
}
