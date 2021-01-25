using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V4.UseCase.Interfaces
{
    public interface IPatchResidentUseCase
    {
        ResidentResponseBoundary Execute(int id, ResidentRequestBoundary command);
    }
}
