using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Boundary.Response;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateHelpRequestUseCase
    {
        HelpRequestResponse Execute(HelpRequest request);
    }
}
