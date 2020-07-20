using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;

namespace cv19ResSupportV3.Tests.V3.Helper
{
    public static class DatabaseEntityHelper
    {
        public static HelpRequestEntity CreateDatabaseEntity()
        {
            var helpRequest = new Fixture().Create<HelpRequest>();

            return CreateDatabaseEntityFrom(helpRequest);
        }

        public static HelpRequestEntity CreateDatabaseEntityFrom(HelpRequest helpRequest)
        {
            return new HelpRequestEntity
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
                HelpWithAccessingMedicine = helpRequest.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequest.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequest.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequest.HelpWithHealth,
                HelpWithMentalHealth = helpRequest.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequest.HelpWithAccessingInternet,
                HelpWithSomethingElse = helpRequest.HelpWithSomethingElse,
                MedicineDeliveryHelpNeeded = helpRequest.MedicineDeliveryHelpNeeded,
                IsPharmacistAbleToDeliver = helpRequest.IsPharmacistAbleToDeliver,
                WhenIsMedicinesDelivered = helpRequest.WhenIsMedicinesDelivered,
                NameAddressPharmacist = helpRequest.NameAddressPharmacist,
                UrgentEssentials = helpRequest.UrgentEssentials,
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
                DateTimeRecorded = helpRequest.DateTimeRecorded
            };
        }
    }
}
