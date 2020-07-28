using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetHelpRequestUseCase
    {
        HelpRequest Execute(int id);
    }
}
