using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetCallbacksUseCase
    {
        List<HelpRequestWithResident> Execute(CallbackQuery requestParams);
    }
}
