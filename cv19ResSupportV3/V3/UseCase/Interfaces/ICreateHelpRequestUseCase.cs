using cv19ResRupportV3.V3.Domain;
using cv19ResRupportV3.V3.Boundary.Response;

namespace cv19ResRupportV3.V3.UseCase.Interfaces
{
    public interface ICreateHelpRequestUseCase
    {
        HelpRequestResponse Execute(HelpRequest request);
    }
}
