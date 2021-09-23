using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class ResponseFactory
    {
        public static HelpRequestResponse ToResponse(this HelpRequest hr)
        {
            if (hr == null)
            {
                return null;
            }

            return new HelpRequestResponse()
            {
                Id = hr.Id,
                ResidentId = hr.ResidentId,
                IsOnBehalf = hr.IsOnBehalf,
                ConsentToCompleteOnBehalf = hr.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = hr.OnBehalfFirstName,
                OnBehalfLastName = hr.OnBehalfLastName,
                OnBehalfEmailAddress = hr.OnBehalfEmailAddress,
                OnBehalfContactNumber = hr.OnBehalfContactNumber,
                RelationshipWithResident = hr.RelationshipWithResident,
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
                WhenIsMedicinesDelivered = hr.WhenIsMedicinesDelivered,
                UrgentEssentials = hr.UrgentEssentials,
                UrgentEssentialsAnythingElse = hr.UrgentEssentialsAnythingElse,
                CurrentSupport = hr.CurrentSupport,
                CurrentSupportFeedback = hr.CurrentSupportFeedback,
                DateTimeRecorded = hr.DateTimeRecorded,
                InitialCallbackCompleted = hr.InitialCallbackCompleted,
                CallbackRequired = hr.CallbackRequired,
                CaseNotes = hr.CaseNotes.ToCaseNotesString(),
                AdviceNotes = hr.AdviceNotes,
                HelpNeeded = hr.HelpNeeded,
                HelpNeededSubtype = hr.HelpNeededSubtype,
                Metadata = hr.Metadata,
                NhsCtasId = hr.NhsCtasId,
                AssignedTo = hr.AssignedTo,
                HelpRequestCalls = hr.HelpRequestCalls
            };
        }

        public static HelpRequestWithResident ToDomain(this HelpRequest helpRequest, Resident resident)
        {
            if (helpRequest == null || resident == null)
            {
                return null;
            }

            return new HelpRequestWithResident
            {
                Id = helpRequest.Id,
                ResidentId = resident.Id,
                IsOnBehalf = helpRequest.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequest.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequest.OnBehalfFirstName,
                OnBehalfLastName = helpRequest.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequest.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequest.OnBehalfContactNumber,
                RelationshipWithResident = helpRequest.RelationshipWithResident,
                Postcode = resident.Postcode,
                Uprn = resident.Uprn,
                Ward = resident.Ward,
                AddressFirstLine = resident.AddressFirstLine,
                AddressSecondLine = resident.AddressSecondLine,
                AddressThirdLine = resident.AddressThirdLine,
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
                IsPharmacistAbleToDeliver = resident.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequest.WhenIsMedicinesDelivered,
                NameAddressPharmacist = resident.NameAddressPharmacist,
                UrgentEssentials = helpRequest.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequest.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequest.CurrentSupport,
                CurrentSupportFeedback = helpRequest.CurrentSupportFeedback,
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
                DateTimeRecorded = helpRequest.DateTimeRecorded,
                RecordStatus = resident.RecordStatus,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CallbackRequired = helpRequest.CallbackRequired,
                CaseNotes = resident.CaseNotes.ToCaseNotesString(),
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                HelpNeededSubtype = helpRequest.HelpNeededSubtype,
                NhsCtasId = helpRequest.NhsCtasId,
                NhsNumber = resident.NhsNumber,
                AssignedTo = helpRequest.AssignedTo,
                Metadata = helpRequest.Metadata,
                HelpRequestCalls = helpRequest.HelpRequestCalls
            };
        }

        public static HelpRequestResponse ToResponse(this HelpRequestWithResident helpRequest)
        {
            if (helpRequest == null)
            {
                return null;
            }

            return new HelpRequestResponse()
            {
                Id = helpRequest.Id,
                ResidentId = helpRequest.ResidentId,
                IsOnBehalf = helpRequest.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequest.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequest.OnBehalfFirstName,
                OnBehalfLastName = helpRequest.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequest.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequest.OnBehalfContactNumber,
                RelationshipWithResident = helpRequest.RelationshipWithResident,
                Postcode = helpRequest.Postcode,
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
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CallbackRequired = helpRequest.CallbackRequired,
                CaseNotes = helpRequest.CaseNotes,
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                HelpNeededSubtype = helpRequest.HelpNeededSubtype,
                NhsCtasId = helpRequest.NhsCtasId,
                Metadata = helpRequest.Metadata,
                NhsNumber = helpRequest.NhsNumber,
                AssignedTo = helpRequest.AssignedTo,
                HelpRequestCalls = helpRequest.HelpRequestCalls
            };
        }

        public static LookupResponse ToResponse(this LookupEntity lookup)
        {
            return lookup == null
                ? null
                : new LookupResponse { LookupGroup = lookup.LookupGroup, Lookup = lookup.Lookup };
        }

        public static List<LookupResponse> ToResponse(this IEnumerable<LookupEntity> lookupEntities)
        {
            return lookupEntities?.Select(le => le.ToResponse()).ToList();
        }

        public static LookupResponse ToResponse(this LookupDomain lookup)
        {
            return lookup == null
                ? null
                : new LookupResponse { LookupGroup = lookup.LookupGroup, Lookup = lookup.Lookup };
        }

        public static List<LookupResponse> ToResponse(this IEnumerable<LookupDomain> lookup)
        {
            return lookup?.Select(l => l.ToResponse()).ToList();
        }

        public static List<HelpRequestResponse> ToResponse(this IEnumerable<HelpRequestWithResident> lookup)
        {
            return lookup?.Select(l => l.ToResponse()).ToList();
        }
    }
}
