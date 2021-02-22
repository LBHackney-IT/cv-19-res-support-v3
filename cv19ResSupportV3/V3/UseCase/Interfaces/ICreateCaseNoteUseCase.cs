using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateCaseNoteUseCase
    {
        ResidentCaseNote Execute(int residentId, int id, string command);
    }
}
