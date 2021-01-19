namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class PatchResidentAndHelpRequest
    {
        public string PostCode { get; set; }
        public string Uprn { get; set; }
        public string Ward { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public string GettingInTouchReason { get; set; }
        public bool? HelpWithAccessingFood { get; set; }
        public bool? HelpWithAccessingSupermarketFood { get; set; }
        public bool? HelpWithCompletingNssForm { get; set; }
        public bool? HelpWithShieldingGuidance { get; set; }
        public bool? HelpWithNoNeedsIdentified { get; set; }
        public bool? HelpWithAccessingMedicine { get; set; }
        public bool? HelpWithAccessingOtherEssentials { get; set; }
        public bool? HelpWithDebtAndMoney { get; set; }
        public bool? HelpWithHealth { get; set; }
        public bool? HelpWithMentalHealth { get; set; }
        public bool? HelpWithAccessingInternet { get; set; }
        public bool? HelpWithSomethingElse { get; set; }
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
        public bool? ConsentToShare { get; set; }
        public string RecordStatus { get; set; }
        public bool? InitialCallbackCompleted { get; set; }
        public bool? CallbackRequired { get; set; }
        public string CaseNotes { get; set; }
        public string AdviceNotes { get; set; }
        public string NhsNumber { get; set; }
        public string NhsCtasId { get; set; }
        public string HelpNeeded { get; set; }
    }
}
