using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class HelpRequestWithResidentFactory
    {
        public static HelpRequestWithResident ToHelpRequestWithResidentDomain(this HelpRequestEntity helpRequest)
        {
            if (helpRequest == null || helpRequest.ResidentEntity == null)
            {
                return null;
            }

            return new HelpRequestWithResident()
            {
                Id = helpRequest.Id,
                ResidentId = helpRequest.ResidentEntity.Id,
                IsOnBehalf = helpRequest.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequest.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequest.OnBehalfFirstName,
                OnBehalfLastName = helpRequest.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequest.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequest.OnBehalfContactNumber,
                RelationshipWithResident = helpRequest.RelationshipWithResident,
                PostCode = helpRequest.ResidentEntity.PostCode,
                Uprn = helpRequest.ResidentEntity.Uprn,
                Ward = helpRequest.ResidentEntity.Ward,
                AddressFirstLine = helpRequest.ResidentEntity.AddressFirstLine,
                AddressSecondLine = helpRequest.ResidentEntity.AddressSecondLine,
                AddressThirdLine = helpRequest.ResidentEntity.AddressThirdLine,
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
                IsPharmacistAbleToDeliver = helpRequest.ResidentEntity.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequest.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequest.ResidentEntity.NameAddressPharmacist,
                UrgentEssentials = helpRequest.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequest.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequest.CurrentSupport,
                CurrentSupportFeedback = helpRequest.CurrentSupportFeedback,
                FirstName = helpRequest.ResidentEntity.FirstName,
                LastName = helpRequest.ResidentEntity.LastName,
                DobMonth = helpRequest.ResidentEntity.DobMonth,
                DobYear = helpRequest.ResidentEntity.DobYear,
                DobDay = helpRequest.ResidentEntity.DobDay,
                ContactTelephoneNumber = helpRequest.ResidentEntity.ContactTelephoneNumber,
                ContactMobileNumber = helpRequest.ResidentEntity.ContactMobileNumber,
                EmailAddress = helpRequest.ResidentEntity.EmailAddress,
                GpSurgeryDetails = helpRequest.ResidentEntity.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequest.ResidentEntity.NumberOfChildrenUnder18,
                ConsentToShare = helpRequest.ResidentEntity.ConsentToShare,
                DateTimeRecorded = helpRequest.DateTimeRecorded,
                RecordStatus = helpRequest.ResidentEntity.RecordStatus,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CallbackRequired = helpRequest.CallbackRequired,
                CaseNotes = helpRequest.ResidentEntity.CaseNotes.ToDomain(),
                AdviceNotes = helpRequest.AdviceNotes,
                HelpNeeded = helpRequest.HelpNeeded,
                NhsCtasId = helpRequest.NhsCtasId,
                NhsNumber = helpRequest.ResidentEntity.NhsNumber,
                HelpRequestCalls = helpRequest.HelpRequestCalls.ToDomain()
            };
        }
    }
}
