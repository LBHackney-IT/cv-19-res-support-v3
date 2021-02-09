using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V4.UseCase.Interface
{
    public interface IGetCaseNotesByResidentIdUseCase
    {
        List<ResidentCaseNote> Execute(int id);
    }
}
