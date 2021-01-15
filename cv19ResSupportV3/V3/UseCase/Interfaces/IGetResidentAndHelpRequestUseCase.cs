using cv19ResSupportV3.V3.Boundary.Response;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetResidentAndHelpRequestUseCase
    {
        HelpRequestResponse Execute(int id);
    }
}
