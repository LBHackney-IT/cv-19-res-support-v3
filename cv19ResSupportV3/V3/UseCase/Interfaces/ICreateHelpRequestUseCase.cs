using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateHelpRequestUseCase
    {
        int Execute(int residentId, CreateHelpRequest command);
    }
}
