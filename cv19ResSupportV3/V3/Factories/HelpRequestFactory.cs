using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;
using HelpRequestCall = cv19ResSupportV3.V3.Domain.HelpRequestCall;

namespace cv19ResSupportV3.V3.Factories
{
    public static class EntityFactory
    {
        public static HelpRequest ToDomain(this HelpRequestCreateRequestBoundary helpRequestEntity)
        {
            return new HelpRequest()
            {
                IsOnBehalf = helpRequestEntity.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequestEntity.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequestEntity.OnBehalfFirstName,
                OnBehalfLastName = helpRequestEntity.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequestEntity.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequestEntity.OnBehalfContactNumber,
                RelationshipWithResident = helpRequestEntity.RelationshipWithResident,
                PostCode = helpRequestEntity.PostCode,
                Uprn = helpRequestEntity.Uprn,
                Ward = helpRequestEntity.Ward,
                AddressFirstLine = helpRequestEntity.AddressFirstLine,
                AddressSecondLine = helpRequestEntity.AddressSecondLine,
                AddressThirdLine = helpRequestEntity.AddressThirdLine,
                GettingInTouchReason = helpRequestEntity.GettingInTouchReason,
                HelpWithAccessingFood = helpRequestEntity.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = helpRequestEntity.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = helpRequestEntity.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = helpRequestEntity.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = helpRequestEntity.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequestEntity.HelpWithHealth,
                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
                HelpWithHousing = helpRequestEntity.HelpWithHousing,
                HelpWithJobsOrTraining = helpRequestEntity.HelpWithJobsOrTraining,
                HelpWithChildrenAndSchools = helpRequestEntity.HelpWithChildrenAndSchools,
                HelpWithDisabilities = helpRequestEntity.HelpWithDisabilities,
                HelpWithSomethingElse = helpRequestEntity.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = helpRequestEntity.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = helpRequestEntity.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequestEntity.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequestEntity.NameAddressPharmacist,
                UrgentEssentials = helpRequestEntity.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequestEntity.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequestEntity.CurrentSupport,
                CurrentSupportFeedback = helpRequestEntity.CurrentSupportFeedback,
                FirstName = helpRequestEntity.FirstName,
                LastName = helpRequestEntity.LastName,
                DobMonth = helpRequestEntity.DobMonth,
                DobYear = helpRequestEntity.DobYear,
                DobDay = helpRequestEntity.DobDay,
                ContactTelephoneNumber = helpRequestEntity.ContactTelephoneNumber,
                ContactMobileNumber = helpRequestEntity.ContactMobileNumber,
                EmailAddress = helpRequestEntity.EmailAddress,
                GpSurgeryDetails = helpRequestEntity.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequestEntity.NumberOfChildrenUnder18,
                ConsentToShare = helpRequestEntity.ConsentToShare,
                DateTimeRecorded = helpRequestEntity.DateTimeRecorded,
                RecordStatus = helpRequestEntity.RecordStatus,
                CallbackRequired = helpRequestEntity.CallbackRequired,
                InitialCallbackCompleted = helpRequestEntity.InitialCallbackCompleted,
                CaseNotes = helpRequestEntity.CaseNotes,
                AdviceNotes = helpRequestEntity.AdviceNotes,
                HelpNeeded = helpRequestEntity.HelpNeeded,
                NhsNumber = helpRequestEntity.NhsNumber,
                NhsCtasId = helpRequestEntity.NhsCtasId,
            };
        }

        public static HelpRequest ToDomain(this HelpRequestUpdateRequest helpRequestEntity)
        {
            return new HelpRequest()
            {
                Id = helpRequestEntity.Id,
                IsOnBehalf = helpRequestEntity.IsOnBehalf,
                ConsentToCompleteOnBehalf = helpRequestEntity.ConsentToCompleteOnBehalf,
                OnBehalfFirstName = helpRequestEntity.OnBehalfFirstName,
                OnBehalfLastName = helpRequestEntity.OnBehalfLastName,
                OnBehalfEmailAddress = helpRequestEntity.OnBehalfEmailAddress,
                OnBehalfContactNumber = helpRequestEntity.OnBehalfContactNumber,
                RelationshipWithResident = helpRequestEntity.RelationshipWithResident,
                PostCode = helpRequestEntity.PostCode,
                Uprn = helpRequestEntity.Uprn,
                Ward = helpRequestEntity.Ward,
                AddressFirstLine = helpRequestEntity.AddressFirstLine,
                AddressSecondLine = helpRequestEntity.AddressSecondLine,
                AddressThirdLine = helpRequestEntity.AddressThirdLine,
                GettingInTouchReason = helpRequestEntity.GettingInTouchReason,
                HelpWithAccessingFood = helpRequestEntity.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = helpRequestEntity.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = helpRequestEntity.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = helpRequestEntity.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = helpRequestEntity.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequestEntity.HelpWithHealth,
                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
                HelpWithHousing = helpRequestEntity.HelpWithHousing,
                HelpWithJobsOrTraining = helpRequestEntity.HelpWithJobsOrTraining,
                HelpWithChildrenAndSchools = helpRequestEntity.HelpWithChildrenAndSchools,
                HelpWithDisabilities = helpRequestEntity.HelpWithDisabilities,
                HelpWithSomethingElse = helpRequestEntity.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = helpRequestEntity.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = helpRequestEntity.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequestEntity.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequestEntity.NameAddressPharmacist,
                UrgentEssentials = helpRequestEntity.UrgentEssentials,
                UrgentEssentialsAnythingElse = helpRequestEntity.UrgentEssentialsAnythingElse,
                CurrentSupport = helpRequestEntity.CurrentSupport,
                CurrentSupportFeedback = helpRequestEntity.CurrentSupportFeedback,
                FirstName = helpRequestEntity.FirstName,
                LastName = helpRequestEntity.LastName,
                DobMonth = helpRequestEntity.DobMonth,
                DobYear = helpRequestEntity.DobYear,
                DobDay = helpRequestEntity.DobDay,
                ContactTelephoneNumber = helpRequestEntity.ContactTelephoneNumber,
                ContactMobileNumber = helpRequestEntity.ContactMobileNumber,
                EmailAddress = helpRequestEntity.EmailAddress,
                GpSurgeryDetails = helpRequestEntity.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequestEntity.NumberOfChildrenUnder18,
                ConsentToShare = helpRequestEntity.ConsentToShare,
                DateTimeRecorded = helpRequestEntity.DateTimeRecorded,
                RecordStatus = helpRequestEntity.RecordStatus,
                CallbackRequired = helpRequestEntity.CallbackRequired,
                InitialCallbackCompleted = helpRequestEntity.InitialCallbackCompleted,
                CaseNotes = helpRequestEntity.CaseNotes,
                AdviceNotes = helpRequestEntity.AdviceNotes,
                HelpNeeded = helpRequestEntity.HelpNeeded,
                NhsNumber = helpRequestEntity.NhsNumber,
                NhsCtasId = helpRequestEntity.NhsCtasId
            };
        }

        public static HelpRequest ToDomain(this HelpRequestPatchRequest helpRequestEntity)
        {
            return new HelpRequest()
            {
                PostCode = helpRequestEntity.PostCode,
                Uprn = helpRequestEntity.Uprn,
                Ward = helpRequestEntity.Ward,
                AddressFirstLine = helpRequestEntity.AddressFirstLine,
                AddressSecondLine = helpRequestEntity.AddressSecondLine,
                AddressThirdLine = helpRequestEntity.AddressThirdLine,
                GettingInTouchReason = helpRequestEntity.GettingInTouchReason,
                HelpWithAccessingFood = helpRequestEntity.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = helpRequestEntity.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = helpRequestEntity.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = helpRequestEntity.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = helpRequestEntity.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequestEntity.HelpWithHealth,
                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
                CurrentSupport = helpRequestEntity.CurrentSupport,
                CurrentSupportFeedback = helpRequestEntity.CurrentSupportFeedback,
                FirstName = helpRequestEntity.FirstName,
                LastName = helpRequestEntity.LastName,
                DobMonth = helpRequestEntity.DobMonth,
                DobYear = helpRequestEntity.DobYear,
                DobDay = helpRequestEntity.DobDay,
                ContactTelephoneNumber = helpRequestEntity.ContactTelephoneNumber,
                ContactMobileNumber = helpRequestEntity.ContactMobileNumber,
                EmailAddress = helpRequestEntity.EmailAddress,
                GpSurgeryDetails = helpRequestEntity.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequestEntity.NumberOfChildrenUnder18,
                ConsentToShare = helpRequestEntity.ConsentToShare,
                DateTimeRecorded = helpRequestEntity.DateTimeRecorded,
                RecordStatus = helpRequestEntity.RecordStatus,
                CallbackRequired = helpRequestEntity.CallbackRequired,
                InitialCallbackCompleted = helpRequestEntity.InitialCallbackCompleted,
                CaseNotes = helpRequestEntity.CaseNotes,
                AdviceNotes = helpRequestEntity.AdviceNotes,
                HelpNeeded = helpRequestEntity.HelpNeeded
            };
        }


        public static List<HelpRequestCallEntity> ToEntity(this ICollection<HelpRequestCall> helpRequestCallList)
        {
            return helpRequestCallList?.Select(hrItem => hrItem.ToEntity()).ToList();
        }
    }
}
