using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;

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
                HelpWithAccessingMedicine = helpRequestEntity.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequestEntity.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequestEntity.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequestEntity.HelpWithHealth,
                HelpWithMentalHealth = helpRequestEntity.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequestEntity.HelpWithAccessingInternet,
                HelpWithSomethingElse = helpRequestEntity.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = helpRequestEntity.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = helpRequestEntity.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequestEntity.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequestEntity.NameAddressPharmacist,
                UrgentEssentials = helpRequestEntity.UrgentEssentials,
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
                DateTimeRecorded = helpRequestEntity.DateTimeRecorded
            };
        }

        public static HelpRequestEntity ToEntity(this HelpRequest helpRequest)
        {
            return new HelpRequestEntity()
            {
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
                RecordStatus = helpRequest.RecordStatus
            };
        }

    }
}
