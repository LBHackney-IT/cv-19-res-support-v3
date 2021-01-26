using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class PatchResidentFactory
    {
        public static PatchResident ToPatchResidentCommand(this CreateResident resident)
        {
            return new PatchResident
            {
                Postcode = resident.Postcode,
                Uprn = resident.Uprn,
                Ward = resident.Ward,
                AddressFirstLine = resident.AddressFirstLine,
                AddressSecondLine = resident.AddressSecondLine,
                AddressThirdLine = resident.AddressThirdLine,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DobMonth = resident.DobMonth,
                DobYear = resident.DobYear,
                DobDay = resident.DobDay,
                ContactTelephoneNumber = resident.ContactTelephoneNumber,
                ContactMobileNumber = resident.ContactMobileNumber,
                EmailAddress = resident.EmailAddress,
                GpSurgeryDetails = resident.GpSurgeryDetails,
                NumberOfChildrenUnder18 = resident.NumberOfChildrenUnder18,
                ConsentToShare = resident.ConsentToShare,
                RecordStatus = resident.RecordStatus,
                IsPharmacistAbleToDeliver = resident.IsPharmacistAbleToDeliver,
                NhsNumber = resident.NhsNumber,
                NameAddressPharmacist = resident.NameAddressPharmacist,
            };
        }
    }
}
