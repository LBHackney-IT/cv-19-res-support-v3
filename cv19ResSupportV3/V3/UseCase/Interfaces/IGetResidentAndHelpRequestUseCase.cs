using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetResidentAndHelpRequestUseCase
    {
        HelpRequestWithResident Execute(int id);
    }
}
