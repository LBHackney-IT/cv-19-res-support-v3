using System;

namespace cv19ResSupportV3.V3.Domain
{
    public class HelpRequest
    {
        public int Id { get; set; }
        public bool IsOnBehalf { get; set; }
        public bool ConsentToCompleteOnBehalf { get; set; }
        public string OnBehalfFirstName { get; set; }
        public string OnBehalfLastName { get; set; }
        public string OnBehalfEmailAddress { get; set; }
        public string OnBehalfContactNumber { get; set; }
        public string RelationshipWithResident { get; set; }
        public string PostCode { get; set; }
        public string Uprn { get; set; }
        public string Ward { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public string GettingInTouchReason { get; set; }
        public bool HelpWithAccessingFood { get; set; }
        public bool HelpWithAccessingMedicine { get; set; }
        public bool HelpWithAccessingOtherEssentials { get; set; }
        public bool HelpWithDebtAndMoney { get; set; }
        public bool HelpWithHealth { get; set; }
        public bool HelpWithMentalHealth { get; set; }
        public bool HelpWithAccessingInternet { get; set; }
        public bool HelpWithSomethingElse { get; set; }
        public bool MedicineDeliveryHelpNeeded { get; set; }
        public bool IsPharmacistAbleToDeliver { get; set; }
        public string WhenIsMedicinesDelivered { get; set; }
        public string NameAddressPharmacist { get; set; }
        public string UrgentEssentials { get; set; }
        public string CurrentSupport { get; set; }
        public string CurrentSupportFeedback { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }
        public string DobDay { get; set; }
        public string ContactTelephoneNumber { get; set; }
        public string ContactMobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string GpSurgeryDetails { get; set; }
        public string NumberOfChildrenUnder18 { get; set; }
        public bool ConsentToShare { get; set; }
        public DateTime DateTimeRecorded { get; set; }
    }
}