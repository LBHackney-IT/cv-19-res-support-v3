using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V4.Factories
{
    public static class ResidentFactory
    {
        public static CreateResident ToCommand(this ResidentRequestBoundary request)
        {
            return request == null ? null : new CreateResident
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DobDay = request.DobDay,
                DobMonth = request.DobMonth,
                DobYear = request.DobYear,
                ContactTelephoneNumber = request.ContactTelephoneNumber,
                ContactMobileNumber = request.ContactMobileNumber,
                EmailAddress = request.EmailAddress,
                AddressFirstLine = request.AddressFirstLine,
                AddressSecondLine = request.AddressSecondLine,
                AddressThirdLine = request.AddressThirdLine,
                Postcode = request.Postcode,
                Uprn = request.Uprn,
                Ward = request.Ward,
                IsPharmacistAbleToDeliver = request.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = request.NameAddressPharmacist,
                GpSurgeryDetails = request.GpSurgeryDetails,
                NumberOfChildrenUnder18 = request.NumberOfChildrenUnder18,
                ConsentToShare = request.ConsentToShare,
                RecordStatus = request.RecordStatus,
                NhsNumber = request.NhsNumber
            };
        }

        public static ResidentResponseBoundary ToResponse(this Resident domain)
        {
            return domain == null ? null : new ResidentResponseBoundary
            {
                Id = domain.Id,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                DobDay = domain.DobDay,
                DobMonth = domain.DobMonth,
                DobYear = domain.DobYear,
                ContactTelephoneNumber = domain.ContactTelephoneNumber,
                ContactMobileNumber = domain.ContactMobileNumber,
                EmailAddress = domain.EmailAddress,
                AddressFirstLine = domain.AddressFirstLine,
                AddressSecondLine = domain.AddressSecondLine,
                AddressThirdLine = domain.AddressThirdLine,
                Postcode = domain.Postcode,
                Uprn = domain.Uprn,
                Ward = domain.Ward,
                IsPharmacistAbleToDeliver = domain.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = domain.NameAddressPharmacist,
                GpSurgeryDetails = domain.GpSurgeryDetails,
                NumberOfChildrenUnder18 = domain.NumberOfChildrenUnder18,
                ConsentToShare = domain.ConsentToShare,
                RecordStatus = domain.RecordStatus,
                NhsNumber = domain.NhsNumber
            };
        }

        public static List<ResidentResponseBoundary> ToResponse(this ICollection<Resident> domain)
        {
            return domain?.Select(d => d.ToResponse()).ToList();
        }

        public static Resident ToResident(this ResidentRequestBoundary request)
        {
            return request == null ? null : new Resident
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DobDay = request.DobDay,
                DobMonth = request.DobMonth,
                DobYear = request.DobYear,
                ContactTelephoneNumber = request.ContactTelephoneNumber,
                ContactMobileNumber = request.ContactMobileNumber,
                EmailAddress = request.EmailAddress,
                AddressFirstLine = request.AddressFirstLine,
                AddressSecondLine = request.AddressSecondLine,
                AddressThirdLine = request.AddressThirdLine,
                Postcode = request.Postcode,
                Uprn = request.Uprn,
                Ward = request.Ward,
                IsPharmacistAbleToDeliver = request.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = request.NameAddressPharmacist,
                GpSurgeryDetails = request.GpSurgeryDetails,
                NumberOfChildrenUnder18 = request.NumberOfChildrenUnder18,
                ConsentToShare = request.ConsentToShare,
                RecordStatus = request.RecordStatus,
                NhsNumber = request.NhsNumber
            };
        }

        public static PatchResident ToPatchResident(this ResidentRequestBoundary request)
        {
            return request == null ? null : new PatchResident
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DobDay = request.DobDay,
                DobMonth = request.DobMonth,
                DobYear = request.DobYear,
                ContactTelephoneNumber = request.ContactTelephoneNumber,
                ContactMobileNumber = request.ContactMobileNumber,
                EmailAddress = request.EmailAddress,
                AddressFirstLine = request.AddressFirstLine,
                AddressSecondLine = request.AddressSecondLine,
                AddressThirdLine = request.AddressThirdLine,
                Postcode = request.Postcode,
                Uprn = request.Uprn,
                Ward = request.Ward,
                IsPharmacistAbleToDeliver = request.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = request.NameAddressPharmacist,
                GpSurgeryDetails = request.GpSurgeryDetails,
                NumberOfChildrenUnder18 = request.NumberOfChildrenUnder18,
                ConsentToShare = request.ConsentToShare,
                RecordStatus = request.RecordStatus,
                NhsNumber = request.NhsNumber
            };
        }
    }
}
