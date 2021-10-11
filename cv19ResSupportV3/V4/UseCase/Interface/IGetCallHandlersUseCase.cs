using System.Collections.Generic;

namespace cv19ResSupportV3.V4.UseCase.Interface
{
    public interface IGetCallHandlersUseCase
    {
        List<CallHandlerResponseBoundary> Execute();
    }
}
