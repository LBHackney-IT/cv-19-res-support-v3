using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("i_need_help_resident_support_v3")]
    public class HelpRequestEntity
    {
        [Column("id", TypeName = "int4")]
        [Key]
        public int Id { get; set; }

        [Column("is_on_behalf", TypeName = "boolean")]
        public bool IsOnBehalf { get; set; }

        [Column("consent_to_complete_on_behalf", TypeName = "boolean")]
        public bool ConsentToCompleteOnBehalf { get; set; }

        [Column("on_behalf_first_name", TypeName = "varchar")]
        public string OnBehalfFirstName { get; set; }

        [Column("on_behalf_last_name", TypeName = "varchar")]
        public string OnBehalfLastName { get; set; }

        [Column("on_behalf_email_address", TypeName = "varchar")]
        public string OnBehalfEmailAddress { get; set; }

        [Column("on_behalf_contact_number", TypeName = "varchar")]
        public string OnBehalfContactNumber { get; set; }

        [Column("relationship_with_resident", TypeName = "varchar")]
        public string RelationshipWithResident { get; set; }

        [Column("postcode", TypeName = "varchar")]
        public string PostCode { get; set; }

        [Column("uprn", TypeName = "varchar")]
        public string Uprn { get; set; }

        [Column("ward", TypeName = "varchar")]
        public string Ward { get; set; }

        [Column("address_first_line", TypeName = "varchar")]
        public string AddressFirstLine { get; set; }

        [Column("address_second_line", TypeName = "varchar")]
        public string AddressSecondLine { get; set; }

        [Column("address_third_line", TypeName = "varchar")]
        public string AddressThirdLine { get; set; }

        [Column("getting_in_touch_reason", TypeName = "text")]
        public string GettingInTouchReason { get; set; }

        [Column("help_with_accessing_food", TypeName = "boolean")]
        public bool HelpWithAccessingFood { get; set; }

        [Column("help_with_accessing_medicine", TypeName = "boolean")]
        public bool HelpWithAccessingMedicine { get; set; }

        [Column("help_with_accessing_other_essentials", TypeName = "boolean")]
        public bool HelpWithAccessingOtherEssentials { get; set; }

        [Column("help_with_debt_and_money", TypeName = "boolean")]
        public bool HelpWithDebtAndMoney { get; set; }

        [Column("help_with_health", TypeName = "boolean")]
        public bool HelpWithHealth { get; set; }

        [Column("help_with_mental_health", TypeName = "boolean")]
        public bool HelpWithMentalHealth { get; set; }

        [Column("help_with_accessing_internet", TypeName = "boolean")]
        public bool HelpWithAccessingInternet { get; set; }

        [Column("help_with_something_else", TypeName = "boolean")]
        public bool HelpWithSomethingElse { get; set; }

        [Column("help_with_housing", TypeName = "boolean")]
        public bool HelpWithHousing { get; set; }

        [Column("help_with_jobs_or_training", TypeName = "boolean")]
        public bool HelpWithJobsOrTraining { get; set; }

        [Column("help_with_children_and_schools", TypeName = "boolean")]
        public bool HelpWithChildrenAndSchools { get; set; }

        [Column("help_with_disabilities", TypeName = "boolean")]
        public bool HelpWithDisabilities { get; set; }

        [Column("medicine_delivery_help_needed", TypeName = "boolean")]
        public bool MedicineDeliveryHelpNeeded { get; set; }

        [Column("is_pharmacist_able_to_deliver", TypeName = "boolean")]
        public bool IsPharmacistAbleToDeliver { get; set; }

        [Column("when_is_medicines_delivered", TypeName = "varchar")]
        public string WhenIsMedicinesDelivered { get; set; }

        [Column("name_address_pharmacist", TypeName = "varchar")]
        public string NameAddressPharmacist { get; set; }

        [Column("urgent_essentials", TypeName = "varchar")]
        public string UrgentEssentials { get; set; }

        [Column("urgent_essentials_anything_else", TypeName = "varchar")]
        public string UrgentEssentialsAnythingElse { get; set; }

        [Column("current_support", TypeName = "varchar")]
        public string CurrentSupport { get; set; }

        [Column("current_support_feedback", TypeName = "text")]
        public string CurrentSupportFeedback { get; set; }

        [Column("first_name", TypeName = "varchar")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "varchar")]
        public string LastName { get; set; }

        [Column("dob_month", TypeName = "varchar")]
        public string DobMonth { get; set; }

        [Column("dob_year", TypeName = "varchar")]
        public string DobYear { get; set; }

        [Column("dob_day", TypeName = "varchar")]
        public string DobDay { get; set; }

        [Column("contact_telephone_number", TypeName = "varchar")]
        public string ContactTelephoneNumber { get; set; }

        [Column("contact_mobile_number", TypeName = "varchar")]
        public string ContactMobileNumber { get; set; }

        [Column("email_address", TypeName = "varchar")]
        public string EmailAddress { get; set; }

        [Column("gp_surgery_details", TypeName = "text")]
        public string GpSurgeryDetails { get; set; }

        [Column("number_of_children_under_18", TypeName = "varchar")]
        public string NumberOfChildrenUnder18 { get; set; }

        [Column("consent_to_share", TypeName = "boolean")]
        public bool ConsentToShare { get; set; }

        [Column("date_time_recorded", TypeName = "timestamp")]
        public DateTime DateTimeRecorded { get; set; }

        [Column("record_status", TypeName = "varchar")]
        public string RecordStatus { get; set; }
    }
}
