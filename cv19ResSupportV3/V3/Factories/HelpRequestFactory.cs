using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;
using HelpRequestCall = cv19ResSupportV3.V3.Domain.HelpRequestCall;

namespace cv19ResSupportV3.V3.Factories
{
    public static class EntityFactory
    {
        public static HelpRequest ToDomain(this HelpRequestEntity helpRequestEntity)
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

        public static HelpRequest ToDomain(this HelpRequestCreateRequestBoundary helpRequestEntity)
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

        public static HelpRequestEntity ToEntity(this HelpRequest helpRequest)
        {
            return new HelpRequestEntity()
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

        public static HelpRequestCallEntity ToEntity(this HelpRequestCall helpRequestCall)
        {
            return new HelpRequestCallEntity()
            {
                Id = helpRequestCall.Id,
                HelpRequestId = helpRequestCall.HelpRequestId,
                CallType = helpRequestCall.CallType,
                CallDirection = helpRequestCall.CallDirection,
                CallOutcome = helpRequestCall.CallOutcome,
                CallDateTime = helpRequestCall.CallDateTime,
                CallHandler = helpRequestCall.CallHandler
            };
        }

        public static HelpRequestCall ToDomain(this HelpRequestCallEntity helpRequestCallEntity)
        {
            return new HelpRequestCall()
            {
                Id = helpRequestCallEntity.Id,
                HelpRequestId = helpRequestCallEntity.HelpRequestId,
                CallType = helpRequestCallEntity.CallType,
                CallDirection = helpRequestCallEntity.CallDirection,
                CallOutcome = helpRequestCallEntity.CallOutcome,
                CallDateTime = helpRequestCallEntity.CallDateTime,
                CallHandler = helpRequestCallEntity.CallHandler
            };
        }

        public static List<HelpRequestCall> ToDomain(this ICollection<HelpRequestCallEntity> helpRequestCallEntityList)
        {
            return helpRequestCallEntityList?.Select(hrItem => hrItem.ToDomain()).ToList();
        }
    }
}
