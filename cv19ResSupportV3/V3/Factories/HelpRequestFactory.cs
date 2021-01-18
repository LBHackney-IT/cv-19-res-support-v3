using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;
using HelpRequestCall = cv19ResSupportV3.V3.Domain.HelpRequestCall;

namespace cv19ResSupportV3.V3.Factories
{
    public static class EntityFactory
    {
        //        public static HelpRequest ToDomain(this HelpRequestCreateRequestBoundary helpRequestEntity)
        //        {
        //            return new HelpRequest()
        //            {
        //                IsOnBehalf = helpRequestEntity.IsOnBehalf,
        //                ConsentToCompleteOnBehalf = helpRequestEntity.ConsentToCompleteOnBehalf,
        //                OnBehalfFirstName = helpRequestEntity.OnBehalfFirstName,
        //                OnBehalfLastName = helpRequestEntity.OnBehalfLastName,
        //                OnBehalfEmailAddress = helpRequestEntity.OnBehalfEmailAddress,
        //                OnBehalfContactNumber = helpRequestEntity.OnBehalfContactNumber,
        //                RelationshipWithResident = helpRequestEntity.RelationshipWithResident,
        //                PostCode = helpRequestEntity.PostCode,
        //                Uprn = helpRequestEntity.Uprn,
        //                Ward = helpRequestEntity.Ward,
        //                AddressFirstLine = helpRequestEntity.AddressFirstLine,
        //                AddressSecondLine = helpRequestEntity.AddressSecondLine,
        //                AddressThirdLine = helpRequestEntity.AddressThirdLine,
        //                GettingInTouchReason = helpRequestEntity.GettingInTouchReason,
        //                HelpWithAccessingFood = helpRequestEntity.HelpWithAccessingFood,
        //                HelpWithAccessingSupermarketFood = helpRequestEntity.HelpWithAccessingSupermarketFood,
        //                HelpWithCompletingNssForm = helpRequestEntity.HelpWithCompletingNssForm,
        //                HelpWithShieldingGuidance = helpRequestEntity.HelpWithShieldingGuidance,
        //                HelpWithNoNeedsIdentified = helpRequestEntity.HelpWithNoNeedsIdentified,
        //                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
        //                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
        //                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
        //                HelpWithHealth = helpRequestEntity.HelpWithHealth,
        //                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
        //                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
        //                HelpWithHousing = helpRequestEntity.HelpWithHousing,
        //                HelpWithJobsOrTraining = helpRequestEntity.HelpWithJobsOrTraining,
        //                HelpWithChildrenAndSchools = helpRequestEntity.HelpWithChildrenAndSchools,
        //                HelpWithDisabilities = helpRequestEntity.HelpWithDisabilities,
        //                HelpWithSomethingElse = helpRequestEntity.HelpWithSomethingElse,
        //                MedicineDeliveryHelpNeeded = helpRequestEntity.MedicineDeliveryHelpNeeded,
        //                IsPharmacistAbleToDeliver = helpRequestEntity.IsPharmacistAbleToDeliver,
        //                WhenIsMedicinesDelivered = helpRequestEntity.WhenIsMedicinesDelivered,
        //                NameAddressPharmacist = helpRequestEntity.NameAddressPharmacist,
        //                UrgentEssentials = helpRequestEntity.UrgentEssentials,
        //                UrgentEssentialsAnythingElse = helpRequestEntity.UrgentEssentialsAnythingElse,
        //                CurrentSupport = helpRequestEntity.CurrentSupport,
        //                CurrentSupportFeedback = helpRequestEntity.CurrentSupportFeedback,
        //                FirstName = helpRequestEntity.FirstName,
        //                LastName = helpRequestEntity.LastName,
        //                DobMonth = helpRequestEntity.DobMonth,
        //                DobYear = helpRequestEntity.DobYear,
        //                DobDay = helpRequestEntity.DobDay,
        //                ContactTelephoneNumber = helpRequestEntity.ContactTelephoneNumber,
        //                ContactMobileNumber = helpRequestEntity.ContactMobileNumber,
        //                EmailAddress = helpRequestEntity.EmailAddress,
        //                GpSurgeryDetails = helpRequestEntity.GpSurgeryDetails,
        //                NumberOfChildrenUnder18 = helpRequestEntity.NumberOfChildrenUnder18,
        //                ConsentToShare = helpRequestEntity.ConsentToShare,
        //                DateTimeRecorded = helpRequestEntity.DateTimeRecorded,
        //                RecordStatus = helpRequestEntity.RecordStatus,
        //                CallbackRequired = helpRequestEntity.CallbackRequired,
        //                InitialCallbackCompleted = helpRequestEntity.InitialCallbackCompleted,
        //                CaseNotes = helpRequestEntity.CaseNotes,
        //                AdviceNotes = helpRequestEntity.AdviceNotes,
        //                HelpNeeded = helpRequestEntity.HelpNeeded,
        //                NhsNumber = helpRequestEntity.NhsNumber,
        //                NhsCtasId = helpRequestEntity.NhsCtasId,
        //            };
        //        }

        public static HelpRequest ToDomain(this HelpRequestEntity helpRequest)
        {
            return new HelpRequest()
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
                WhenIsMedicinesDelivered = helpRequest.WhenIsMedicinesDelivered,
                UrgentEssentials = helpRequest.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequest.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequest.CurrentSupport,
                CurrentSupportFeedback = helpRequest.CurrentSupportFeedback,
                DateTimeRecorded = helpRequest.DateTimeRecorded,
                CallbackRequired = helpRequest.CallbackRequired,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                HelpRequestCalls = helpRequest.HelpRequestCalls.ToDomain(),
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                NhsCtasId = helpRequest.NhsCtasId
            };
        }
        public static Resident ToDomain(this ResidentEntity resident)
        {
            return new Resident()
            {
                Id = resident.Id,
                PostCode = resident.PostCode,
                Uprn = resident.Uprn,
                Ward = resident.Ward,
                AddressFirstLine = resident.AddressFirstLine,
                AddressSecondLine = resident.AddressSecondLine,
                AddressThirdLine = resident.AddressThirdLine,
                IsPharmacistAbleToDeliver = resident.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = resident.NameAddressPharmacist,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DobMonth = resident.DobMonth,
                DobYear = resident.DobYear,
                DobDay = resident.DobDay,
                ContactTelephoneNumber = resident.ContactTelephoneNumber,
                ContactMobileNumber = resident.ContactMobileNumber,
                EmailAddress = resident.EmailAddress,
                GpSurgeryDetails = resident.GpSurgeryDetails,
                RecordStatus = resident.RecordStatus,
                NumberOfChildrenUnder18 = resident.NumberOfChildrenUnder18,
                ConsentToShare = resident.ConsentToShare,
                CaseNotes = resident.CaseNotes.ToDomain(),
                NhsNumber = resident.NhsNumber,
            };
        }

        //        public static HelpRequest ToDomain(this HelpRequestPatchRequest helpRequestEntity)
        //        {
        //            return new HelpRequest()
        //            {
        //                PostCode = helpRequestEntity.PostCode,
        //                Uprn = helpRequestEntity.Uprn,
        //                Ward = helpRequestEntity.Ward,
        //                AddressFirstLine = helpRequestEntity.AddressFirstLine,
        //                AddressSecondLine = helpRequestEntity.AddressSecondLine,
        //                AddressThirdLine = helpRequestEntity.AddressThirdLine,
        //                GettingInTouchReason = helpRequestEntity.GettingInTouchReason,
        //                HelpWithAccessingFood = helpRequestEntity.HelpWithAccessingFood,
        //                HelpWithAccessingSupermarketFood = helpRequestEntity.HelpWithAccessingSupermarketFood,
        //                HelpWithCompletingNssForm = helpRequestEntity.HelpWithCompletingNssForm,
        //                HelpWithShieldingGuidance = helpRequestEntity.HelpWithShieldingGuidance,
        //                HelpWithNoNeedsIdentified = helpRequestEntity.HelpWithNoNeedsIdentified,
        //                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
        //                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
        //                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
        //                HelpWithHealth = helpRequestEntity.HelpWithHealth,
        //                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
        //                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
        //                CurrentSupport = helpRequestEntity.CurrentSupport,
        //                CurrentSupportFeedback = helpRequestEntity.CurrentSupportFeedback,
        //                FirstName = helpRequestEntity.FirstName,
        //                LastName = helpRequestEntity.LastName,
        //                DobMonth = helpRequestEntity.DobMonth,
        //                DobYear = helpRequestEntity.DobYear,
        //                DobDay = helpRequestEntity.DobDay,
        //                ContactTelephoneNumber = helpRequestEntity.ContactTelephoneNumber,
        //                ContactMobileNumber = helpRequestEntity.ContactMobileNumber,
        //                EmailAddress = helpRequestEntity.EmailAddress,
        //                GpSurgeryDetails = helpRequestEntity.GpSurgeryDetails,
        //                NumberOfChildrenUnder18 = helpRequestEntity.NumberOfChildrenUnder18,
        //                ConsentToShare = helpRequestEntity.ConsentToShare,
        //                DateTimeRecorded = helpRequestEntity.DateTimeRecorded,
        //                RecordStatus = helpRequestEntity.RecordStatus,
        //                CallbackRequired = helpRequestEntity.CallbackRequired,
        //                InitialCallbackCompleted = helpRequestEntity.InitialCallbackCompleted,
        //                CaseNotes = helpRequestEntity.CaseNotes,
        //                AdviceNotes = helpRequestEntity.AdviceNotes,
        //                HelpNeeded = helpRequestEntity.HelpNeeded
        //            };
        //        }


        public static List<HelpRequestCallEntity> ToEntity(this ICollection<HelpRequestCall> helpRequestCallList)
        {
            return helpRequestCallList?.Select(hrItem => hrItem.ToEntity()).ToList();
        }

        public static ResidentCaseNote ToDomain(this CaseNoteEntity caseNote)
        {
            return new ResidentCaseNote()
            {
                Id = caseNote.Id,
                CaseNote = caseNote.CaseNote,
                HelpRequestId = caseNote.HelpRequestId,
                ResidentId = caseNote.ResidentId
            };
        }

        public static List<ResidentCaseNote> ToDomain(this ICollection<CaseNoteEntity> caseNotes)
        {
            return caseNotes?.Select(hrItem => hrItem.ToDomain()).ToList();
        }

        public static string ToCaseNotesString(this ICollection<CaseNoteEntity> caseNotes)
        {
            return caseNotes == null ? null : string.Join(" ", caseNotes?.Select(item => item.CaseNote));
        }
        public static string ToCaseNotesString(this ICollection<ResidentCaseNote> caseNotes)
        {
            return caseNotes == null ? null : string.Join(" ", caseNotes?.Select(item => item.CaseNote));
        }

        public static List<HelpRequest> ToDomain(this ICollection<HelpRequestEntity> helpRequests)
        {
            return helpRequests?.Select(hrItem => hrItem.ToDomain()).ToList();
        }
    }
}
