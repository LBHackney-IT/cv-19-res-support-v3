using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IResidentGateway
    {
        int? FindResident(FindResident command);
        Resident CreateResident(CreateResident command);
        Resident UpdateResident(int residentId, UpdateResident command);
        Resident GetResident(int id);
        Resident PatchResident(int id, PatchResident command);
        List<Resident> SearchResidents(FindResident command);
    }
}
