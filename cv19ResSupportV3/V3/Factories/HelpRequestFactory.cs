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
                NhsCtasId = helpRequest.NhsCtasId,
                AssignedTo = helpRequest.AssignedTo,
            };
        }
        public static Resident ToDomain(this ResidentEntity resident)
        {
            return new Resident()
            {
                Id = resident.Id,
                Postcode = resident.Postcode,
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
    }
}
