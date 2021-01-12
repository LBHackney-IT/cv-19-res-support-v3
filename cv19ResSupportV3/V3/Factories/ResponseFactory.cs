using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class ResponseFactory
    {
        public static HelpRequestResponse ToResponse(this HelpRequestEntity hr)
        {
            if (hr == null)
            {
                return null;
            }
            return new HelpRequestResponse()
            {
                Id = hr.Id,
                IsOnBehalf = hr.IsOnBehalf,
                ConsentToCompleteOnBehalf = hr.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = hr.OnBehalfFirstName,
                OnBehalfLastName = hr.OnBehalfLastName,
                OnBehalfEmailAddress = hr.OnBehalfEmailAddress,
                OnBehalfContactNumber = hr.OnBehalfContactNumber,
                RelationshipWithResident = hr.RelationshipWithResident,
                PostCode = hr.PostCode,
                Uprn = hr.Uprn,
                Ward = hr.Ward,
                AddressFirstLine = hr.AddressFirstLine,
                AddressSecondLine = hr.AddressSecondLine,
                AddressThirdLine = hr.AddressThirdLine,
                GettingInTouchReason = hr.GettingInTouchReason,
                HelpWithAccessingFood = hr.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = hr.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = hr.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = hr.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = hr.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = hr.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = hr.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = hr.HelpWithDebtAndMoney,
                HelpWithHealth = hr.HelpWithHealth,
                HelpWithMentalHealth = hr.HelpWithMentalHealth,
                HelpWithAccessingInternet = hr.HelpWithAccessingInternet,
                HelpWithHousing = hr.HelpWithHousing,
                HelpWithJobsOrTraining = hr.HelpWithJobsOrTraining,
                HelpWithChildrenAndSchools = hr.HelpWithChildrenAndSchools,
                HelpWithDisabilities = hr.HelpWithDisabilities,
                HelpWithSomethingElse = hr.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = hr.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = hr.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = hr.WhenIsMedicinesDelivered,
                NameAddressPharmacist = hr.NameAddressPharmacist,
                UrgentEssentials = hr.UrgentEssentials,
                UrgentEssentialsAnythingElse = hr.UrgentEssentialsAnythingElse,
                CurrentSupport = hr.CurrentSupport,
                CurrentSupportFeedback = hr.CurrentSupportFeedback,
                FirstName = hr.FirstName,
                LastName = hr.LastName,
                DobMonth = hr.DobMonth,
                DobYear = hr.DobYear,
                DobDay = hr.DobDay,
                ContactTelephoneNumber = hr.ContactTelephoneNumber,
                ContactMobileNumber = hr.ContactMobileNumber,
                EmailAddress = hr.EmailAddress,
                GpSurgeryDetails = hr.GpSurgeryDetails,
                NumberOfChildrenUnder18 = hr.NumberOfChildrenUnder18,
                ConsentToShare = hr.ConsentToShare,
                DateTimeRecorded = hr.DateTimeRecorded,
                RecordStatus = hr.RecordStatus,
                InitialCallbackCompleted = hr.InitialCallbackCompleted,
                CallbackRequired = hr.CallbackRequired,
                CaseNotes = hr.CaseNotes,
                AdviceNotes = hr.AdviceNotes,
                HelpNeeded = hr.HelpNeeded,
                NhsCtasId = hr.NhsCtasId,
                NhsNumber = hr.NhsNumber,
                HelpRequestCalls = hr.HelpRequestCalls.ToDomain()
            };
        }

        public static HelpRequestResponse ToResponse(this HelpRequest hr)

        {
            if (hr == null)
            {
                return null;
            }

            return new HelpRequestResponse()
            {
                Id = hr.Id,
                IsOnBehalf = hr.IsOnBehalf,
                ConsentToCompleteOnBehalf = hr.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = hr.OnBehalfFirstName,
                OnBehalfLastName = hr.OnBehalfLastName,
                OnBehalfEmailAddress = hr.OnBehalfEmailAddress,
                OnBehalfContactNumber = hr.OnBehalfContactNumber,
                RelationshipWithResident = hr.RelationshipWithResident,
                PostCode = hr.PostCode,
                Uprn = hr.Uprn,
                Ward = hr.Ward,
                AddressFirstLine = hr.AddressFirstLine,
                AddressSecondLine = hr.AddressSecondLine,
                AddressThirdLine = hr.AddressThirdLine,
                GettingInTouchReason = hr.GettingInTouchReason,
                HelpWithAccessingFood = hr.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = hr.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = hr.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = hr.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = hr.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = hr.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = hr.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = hr.HelpWithDebtAndMoney,
                HelpWithHealth = hr.HelpWithHealth,
                HelpWithMentalHealth = hr.HelpWithMentalHealth,
                HelpWithAccessingInternet = hr.HelpWithAccessingInternet,
                HelpWithHousing = hr.HelpWithHousing,
                HelpWithJobsOrTraining = hr.HelpWithJobsOrTraining,
                HelpWithChildrenAndSchools = hr.HelpWithChildrenAndSchools,
                HelpWithDisabilities = hr.HelpWithDisabilities,
                HelpWithSomethingElse = hr.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = hr.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = hr.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = hr.WhenIsMedicinesDelivered,
                NameAddressPharmacist = hr.NameAddressPharmacist,
                UrgentEssentials = hr.UrgentEssentials,
                UrgentEssentialsAnythingElse = hr.UrgentEssentialsAnythingElse,
                CurrentSupport = hr.CurrentSupport,
                CurrentSupportFeedback = hr.CurrentSupportFeedback,
                FirstName = hr.FirstName,
                LastName = hr.LastName,
                DobMonth = hr.DobMonth,
                DobYear = hr.DobYear,
                DobDay = hr.DobDay,
                ContactTelephoneNumber = hr.ContactTelephoneNumber,
                ContactMobileNumber = hr.ContactMobileNumber,
                EmailAddress = hr.EmailAddress,
                GpSurgeryDetails = hr.GpSurgeryDetails,
                NumberOfChildrenUnder18 = hr.NumberOfChildrenUnder18,
                ConsentToShare = hr.ConsentToShare,
                DateTimeRecorded = hr.DateTimeRecorded,
                RecordStatus = hr.RecordStatus,
                InitialCallbackCompleted = hr.InitialCallbackCompleted,
                CallbackRequired = hr.CallbackRequired,
                CaseNotes = hr.CaseNotes,
                AdviceNotes = hr.AdviceNotes,
                HelpNeeded = hr.HelpNeeded,
                NhsCtasId = hr.NhsCtasId,
                NhsNumber = hr.NhsNumber,
                HelpRequestCalls = hr.HelpRequestCalls
            };
        }

        public static List<HelpRequestResponse> ToResponse(this IEnumerable<HelpRequestEntity> responseList)
        {
            return responseList.Select(responseItem => responseItem.ToResponse()).ToList();
        }

        public static List<HelpRequest> ToDomain(this IEnumerable<HelpRequestEntity> responseList)
        {
            return responseList.Select(responseItem => responseItem.ToDomain()).ToList();
        }

        public static List<HelpRequestResponse> ToResponse(this IEnumerable<HelpRequest> responseList)
        {
            return responseList.Select(responseItem => responseItem.ToResponse()).ToList();
        }

        public static LookupResponse ToResponse(this LookupEntity lookup)
        {
            return lookup == null
                ? null
                : new LookupResponse { LookupGroup = lookup.LookupGroup, Lookup = lookup.Lookup };
        }

        public static List<LookupResponse> ToResponse(this IEnumerable<LookupEntity> lookupEntities)
        {
            return lookupEntities.Select(le => le.ToResponse()).ToList();
        }

        public static LookupResponse ToResponse(this LookupDomain lookup)
        {
            return lookup == null
                ? null
                : new LookupResponse { LookupGroup = lookup.LookupGroup, Lookup = lookup.Lookup };
        }

        public static List<LookupResponse> ToResponse(this IEnumerable<LookupDomain> lookup)
        {
            return lookup.Select(l => l.ToResponse()).ToList();
        }
    }
}
