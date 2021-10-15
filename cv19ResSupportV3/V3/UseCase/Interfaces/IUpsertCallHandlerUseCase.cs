using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V4;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface IUpsertCallHandlerUseCase
    {
        CallHandler Execute(CallHandlerRequestBoundary request);
    }
}
