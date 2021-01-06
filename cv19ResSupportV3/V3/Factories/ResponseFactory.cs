using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class ResponseFactory
    {
        public static HelpRequestGetResponse ToResponse(this HelpRequestEntity hr)
        {
            if (hr == null)
            {
                return null;
            }
            return new HelpRequestGetResponse()
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

        public static HelpRequestUpdateResponse ToResponse(this HelpRequest helpRequest)
        {
            return new HelpRequestUpdateResponse()
            {
                Id = helpRequest.Id,
                IsOnBehalf = helpRequest.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequest.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequest.OnBehalfFirstName,
                OnBehalfLastName = helpRequest.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequest.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequest.OnBehalfContactNumber,
                RelationshipWithResident = helpRequest.RelationshipWithResident,
                PostCode = helpRequest.PostCode,
                Uprn = helpRequest.Uprn,
                Ward = helpRequest.Ward,
                AddressFirstLine = helpRequest.AddressFirstLine,
                AddressSecondLine = helpRequest.AddressSecondLine,
                AddressThirdLine = helpRequest.AddressThirdLine,
                GettingInTouchReason = helpRequest.GettingInTouchReason,
                HelpWithAccessingFood = helpRequest.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = helpRequest.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = helpRequest.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = helpRequest.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = helpRequest.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = helpRequest.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequest.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequest.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequest.HelpWithHealth,
                HelpWithMentalHealth = helpRequest.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequest.HelpWithAccessingInternet,
                HelpWithHousing = helpRequest.HelpWithHousing,
                HelpWithJobsOrTraining = helpRequest.HelpWithJobsOrTraining,
                HelpWithChildrenAndSchools = helpRequest.HelpWithChildrenAndSchools,
                HelpWithDisabilities = helpRequest.HelpWithDisabilities,
                HelpWithSomethingElse = helpRequest.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = helpRequest.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = helpRequest.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequest.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequest.NameAddressPharmacist,
                UrgentEssentials = helpRequest.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequest.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequest.CurrentSupport,
                CurrentSupportFeedback = helpRequest.CurrentSupportFeedback,
                FirstName = helpRequest.FirstName,
                LastName = helpRequest.LastName,
                DobMonth = helpRequest.DobMonth,
                DobYear = helpRequest.DobYear,
                DobDay = helpRequest.DobDay,
                ContactTelephoneNumber = helpRequest.ContactTelephoneNumber,
                ContactMobileNumber = helpRequest.ContactMobileNumber,
                EmailAddress = helpRequest.EmailAddress,
                GpSurgeryDetails = helpRequest.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequest.NumberOfChildrenUnder18,
                ConsentToShare = helpRequest.ConsentToShare,
                DateTimeRecorded = helpRequest.DateTimeRecorded,
                RecordStatus = helpRequest.RecordStatus,
                CallbackRequired = helpRequest.CallbackRequired,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CaseNotes = helpRequest.CaseNotes,
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                NhsNumber = helpRequest.NhsNumber,
                NhsCtasId = helpRequest.NhsCtasId
            };
        }

        public static List<HelpRequestGetResponse> ToResponse(this IEnumerable<HelpRequestEntity> responseList)
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
    }
}
