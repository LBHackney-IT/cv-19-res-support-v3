using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface IPatchCaseNoteUseCase
    {
        ResidentCaseNote Execute(int id, int residentId, string command);
    }
}
