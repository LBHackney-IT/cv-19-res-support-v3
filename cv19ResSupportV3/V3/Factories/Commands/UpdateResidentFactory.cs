using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class MergeResidentFactory
    {
        public static UpdateResident ToUpdateResidentCommand(this CreateResidentAndHelpRequest helpRequest)
        {
            return new UpdateResident()
            {
                PostCode = helpRequest.PostCode,
                Uprn = helpRequest.Uprn,
                Ward = helpRequest.Ward,
                AddressFirstLine = helpRequest.AddressFirstLine,
                AddressSecondLine = helpRequest.AddressSecondLine,
                AddressThirdLine = helpRequest.AddressThirdLine,
                IsPharmacistAbleToDeliver = helpRequest.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = helpRequest.NameAddressPharmacist,
                DobMonth = helpRequest.DobMonth,
                DobYear = helpRequest.DobYear,
                DobDay = helpRequest.DobDay,
                ContactTelephoneNumber = helpRequest.ContactTelephoneNumber,
                ContactMobileNumber = helpRequest.ContactMobileNumber,
                EmailAddress = helpRequest.EmailAddress,
                GpSurgeryDetails = helpRequest.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequest.NumberOfChildrenUnder18,
                ConsentToShare = helpRequest.ConsentToShare,
                RecordStatus = helpRequest.RecordStatus,
                NhsNumber = helpRequest.NhsNumber,
            };
        }
        public static UpdateResident ToUpdateResidentCommand(this CreateResident helpRequest)
        {
            return new UpdateResident()
            {
                PostCode = helpRequest.PostCode,
                Uprn = helpRequest.Uprn,
                Ward = helpRequest.Ward,
                AddressFirstLine = helpRequest.AddressFirstLine,
                AddressSecondLine = helpRequest.AddressSecondLine,
                AddressThirdLine = helpRequest.AddressThirdLine,
                IsPharmacistAbleToDeliver = helpRequest.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = helpRequest.NameAddressPharmacist,
                DobMonth = helpRequest.DobMonth,
                DobYear = helpRequest.DobYear,
                DobDay = helpRequest.DobDay,
                ContactTelephoneNumber = helpRequest.ContactTelephoneNumber,
                ContactMobileNumber = helpRequest.ContactMobileNumber,
                EmailAddress = helpRequest.EmailAddress,
                GpSurgeryDetails = helpRequest.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequest.NumberOfChildrenUnder18,
                ConsentToShare = helpRequest.ConsentToShare,
                RecordStatus = helpRequest.RecordStatus,
                NhsNumber = helpRequest.NhsNumber,
            };
        }
    }
}