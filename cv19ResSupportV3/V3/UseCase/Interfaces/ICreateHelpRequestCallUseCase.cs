using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateHelpRequestCallUseCase
    {
        int Execute(int id, CreateHelpRequestCall request);
    }
}
