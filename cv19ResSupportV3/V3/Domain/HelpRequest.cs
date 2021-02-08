using System;
using System.Collections.Generic;

namespace cv19ResSupportV3.V3.Domain
{
    public class HelpRequest
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public bool? IsOnBehalf { get; set; }
        public bool? ConsentToCompleteOnBehalf { get; set; }
        public string OnBehalfFirstName { get; set; }
        public string OnBehalfLastName { get; set; }
        public string OnBehalfEmailAddress { get; set; }
        public string OnBehalfContactNumber { get; set; }
        public string RelationshipWithResident { get; set; }
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
        public bool? HelpWithHousing { get; set; }
        public bool? HelpWithJobsOrTraining { get; set; }
        public bool? HelpWithChildrenAndSchools { get; set; }
        public bool? HelpWithDisabilities { get; set; }
        public bool? HelpWithSomethingElse { get; set; }
        public bool? MedicineDeliveryHelpNeeded { get; set; }
        public string WhenIsMedicinesDelivered { get; set; }
        public string UrgentEssentials { get; set; }
        public string UrgentEssentialsAnythingElse { get; set; }
        public string CurrentSupport { get; set; }
        public string CurrentSupportFeedback { get; set; }
        public DateTime? DateTimeRecorded { get; set; }
        public bool? InitialCallbackCompleted { get; set; }
        public bool? CallbackRequired { get; set; }
        public List<ResidentCaseNote> CaseNotes { get; set; }
        public string AdviceNotes { get; set; }
        public string HelpNeeded { get; set; }
        public string NhsCtasId { get; set; }
        public string AssignedTo { get; set; }
        public dynamic Metadata { get; set; }
        public List<HelpRequestCall> HelpRequestCalls { get; set; }
    }
}
