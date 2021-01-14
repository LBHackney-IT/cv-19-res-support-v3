using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase.Interfaces
{
    public interface IMergeResidentUseCase
    {
        Resident Execute(int resident_id, UpdateResident command);
    }
}
