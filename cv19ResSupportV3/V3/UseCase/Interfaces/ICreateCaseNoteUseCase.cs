using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateCaseNoteUseCase
    {
        ResidentCaseNote Execute(int id, int residentId, string command);
    }
}
