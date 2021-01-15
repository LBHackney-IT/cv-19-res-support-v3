using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class ResidentDomain
    {
        public static Resident ToResidentDomain(this ResidentEntity residentEntity)
        {
            return new Resident
            {
                Id = residentEntity.Id,
                PostCode = residentEntity.PostCode,
                Uprn = residentEntity.Uprn,
                Ward = residentEntity.Ward,
                AddressFirstLine = residentEntity.AddressFirstLine,
                AddressSecondLine = residentEntity.AddressSecondLine,
                AddressThirdLine = residentEntity.AddressThirdLine,
                IsPharmacistAbleToDeliver = residentEntity.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = residentEntity.NameAddressPharmacist,
                FirstName = residentEntity.FirstName,
                LastName = residentEntity.LastName,
                DobMonth = residentEntity.DobMonth,
                DobYear = residentEntity.DobYear,
                DobDay = residentEntity.DobDay,
                ContactTelephoneNumber = residentEntity.ContactTelephoneNumber,
                ContactMobileNumber = residentEntity.ContactMobileNumber,
                EmailAddress = residentEntity.EmailAddress,
                GpSurgeryDetails = residentEntity.GpSurgeryDetails,
                NumberOfChildrenUnder18 = residentEntity.NumberOfChildrenUnder18,
                ConsentToShare = residentEntity.ConsentToShare,
                NhsNumber = residentEntity.NhsNumber,
                CaseNotes = residentEntity.CaseNotes.ToDomain()
            };
        }
        public static ResidentEntity ToResidentEntity(this CreateResident command)
        {
            return new ResidentEntity
            {
                PostCode = command.PostCode,
                Uprn = command.Uprn,
                Ward = command.Ward,
                AddressFirstLine = command.AddressFirstLine,
                AddressSecondLine = command.AddressSecondLine,
                AddressThirdLine = command.AddressThirdLine,
                IsPharmacistAbleToDeliver = command.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = command.NameAddressPharmacist,
                FirstName = command.FirstName,
                LastName = command.LastName,
                DobMonth = command.DobMonth,
                DobYear = command.DobYear,
                DobDay = command.DobDay,
                ContactTelephoneNumber = command.ContactTelephoneNumber,
                ContactMobileNumber = command.ContactMobileNumber,
                EmailAddress = command.EmailAddress,
                GpSurgeryDetails = command.GpSurgeryDetails,
                NumberOfChildrenUnder18 = command.NumberOfChildrenUnder18,
                ConsentToShare = command.ConsentToShare,
                NhsNumber = command.NhsNumber
            };
        }
    }
}
