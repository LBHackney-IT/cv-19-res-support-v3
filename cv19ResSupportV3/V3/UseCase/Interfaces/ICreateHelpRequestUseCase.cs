using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface ICreateHelpRequestUseCase
    {
        int Execute(int resident_id, CreateHelpRequest command);
    }
}
