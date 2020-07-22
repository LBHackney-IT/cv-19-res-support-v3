using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("i_need_help_resident_support_v3")]
    public class HelpRequestEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("is_on_behalf")]
        public bool IsOnBehalf { get; set; }

        [Column("consent_to_complete_on_behalf")]
        public bool ConsentToCompleteOnBehalf { get; set; }

        [Column("on_behalf_first_name")]
        public string OnBehalfFirstName { get; set; }

        [Column("on_behalf_last_name")]
        public string OnBehalfLastName { get; set; }

        [Column("on_behalf_email_address")]
        public string OnBehalfEmailAddress { get; set; }

        [Column("on_behalf_contact_number")]
        public string OnBehalfContactNumber { get; set; }

        [Column("relationship_with_resident")]
        public string RelationshipWithResident { get; set; }

        [Column("postcode")]
        public string PostCode { get; set; }

        [Column("uprn")]
        public string Uprn { get; set; }

        [Column("ward")]
        public string Ward { get; set; }

        [Column("address_first_line")]
        public string AddressFirstLine { get; set; }

        [Column("address_second_line")]
        public string AddressSecondLine { get; set; }

        [Column("address_third_line")]
        public string AddressThirdLine { get; set; }

        [Column("getting_in_touch_reason")]
        public string GettingInTouchReason { get; set; }

        [Column("help_with_accessing_food")]
        public bool HelpWithAccessingFood { get; set; }

        [Column("help_with_accessing_medicine")]
        public bool HelpWithAccessingMedicine { get; set; }

        [Column("help_with_accessing_other_essentials")]
        public bool HelpWithAccessingOtherEssentials { get; set; }

        [Column("help_with_debt_and_money")]
        public bool HelpWithDebtAndMoney { get; set; }

        [Column("help_with_health")]
        public bool HelpWithHealth { get; set; }

        [Column("help_with_mental_health")]
        public bool HelpWithMentalHealth { get; set; }

        [Column("help_with_accessing_internet")]
        public bool HelpWithAccessingInternet { get; set; }

        [Column("help_with_something_else")]
        public bool HelpWithSomethingElse { get; set; }

        [Column("help_with_housing")]
        public bool HelpWithHousing { get; set; }

        [Column("help_with_jobs_or_training")]
        public bool HelpWithJobsOrTraining { get; set; }

        [Column("help_with_children_and_schools")]
        public bool HelpWithChildrenAndSchools { get; set; }

        [Column("help_with_disabilities")]
        public bool HelpWithDisabilities { get; set; }

        [Column("medicine_delivery_help_needed")]
        public bool MedicineDeliveryHelpNeeded { get; set; }

        [Column("is_pharmacist_able_to_deliver")]
        public bool IsPharmacistAbleToDeliver { get; set; }

        [Column("when_is_medicines_delivered")]
        public string WhenIsMedicinesDelivered { get; set; }

        [Column("name_address_pharmacist")]
        public string NameAddressPharmacist { get; set; }

        [Column("urgent_essentials")]
        public string UrgentEssentials { get; set; }

        [Column("urgent_essentials_anything_else")]
        public string UrgentEssentialsAnythingElse { get; set; }

        [Column("current_support")]
        public string CurrentSupport { get; set; }

        [Column("current_support_feedback")]
        public string CurrentSupportFeedback { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("dob_month")]
        public string DobMonth { get; set; }

        [Column("dob_year")]
        public string DobYear { get; set; }

        [Column("dob_day")]
        public string DobDay { get; set; }

        [Column("contact_telephone_number")]
        public string ContactTelephoneNumber { get; set; }

        [Column("contact_mobile_number")]
        public string ContactMobileNumber { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Column("gp_surgery_details")]
        public string GpSurgeryDetails { get; set; }

        [Column("number_of_children_under_18")]
        public string NumberOfChildrenUnder18 { get; set; }

        [Column("consent_to_share")]
        public bool ConsentToShare { get; set; }

        [Column("date_time_recorded")]
        public DateTime DateTimeRecorded { get; set; }

    }
}
