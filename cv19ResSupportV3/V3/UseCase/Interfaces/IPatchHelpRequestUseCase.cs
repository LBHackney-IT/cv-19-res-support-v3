using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface IPatchHelpRequestUseCase
    {
        HelpRequest Execute(int id, PatchHelpRequest command);
    }
}
