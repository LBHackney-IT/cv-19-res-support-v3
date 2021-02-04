using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.Tests.V3.Helpers
{
    public static class FactoryHelper
    {
        public static HelpRequestWithResident ToDomain(this ResidentEntity resident, HelpRequestEntity helpRequest)
        {
            return new HelpRequestWithResident()
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
                Metadata = helpRequest.Metadata,
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
                CallbackRequired = helpRequest.CallbackRequired,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CaseNotes = resident.CaseNotes.ToCaseNotesString(),
                HelpRequestCalls = helpRequest.HelpRequestCalls.ToDomain(),
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                NhsNumber = resident.NhsNumber,
                NhsCtasId = helpRequest.NhsCtasId,
                AssignedTo = helpRequest.AssignedTo
            };
        }
    }
}
