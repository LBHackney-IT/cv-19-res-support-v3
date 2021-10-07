using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("help_requests")]
    public class HelpRequestEntity
    {
        public HelpRequestEntity()
        {
            CaseNotes = new List<CaseNoteEntity>();
            HelpRequestCalls = new List<HelpRequestCallEntity>();

        }
        [Column("id")]
        [Key] public int Id { get; set; }

        [Column("resident_id")] [ForeignKey("ResidentEntity")] public int ResidentId { get; set; }

        [Column("call_handler_id")] [ForeignKey("CallHandlerEntity")] public int? CallHandlerId { get; set; }

        [Column("is_on_behalf")] public bool? IsOnBehalf { get; set; }

        [Column("consent_to_complete_on_behalf")]
        public bool? ConsentToCompleteOnBehalf { get; set; }

        [Column("on_behalf_first_name")] public string OnBehalfFirstName { get; set; }

        [Column("on_behalf_last_name")] public string OnBehalfLastName { get; set; }

        [Column("on_behalf_email_address")] public string OnBehalfEmailAddress { get; set; }

        [Column("on_behalf_contact_number")] public string OnBehalfContactNumber { get; set; }

        [Column("relationship_with_resident")] public string RelationshipWithResident { get; set; }

        [Column("getting_in_touch_reason")] public string GettingInTouchReason { get; set; }

        [Column("help_with_accessing_food")]
        public bool? HelpWithAccessingFood { get; set; }

        [Column("help_with_accessing_supermarket_food")]
        public bool? HelpWithAccessingSupermarketFood { get; set; }

        [Column("help_with_completing_nss_form")]
        public bool? HelpWithCompletingNssForm { get; set; }

        [Column("help_with_shielding_guidance")]
        public bool? HelpWithShieldingGuidance { get; set; }

        [Column("help_with_no_needs_identified")]
        public bool? HelpWithNoNeedsIdentified { get; set; }

        [Column("help_with_accessing_medicine")]
        public bool? HelpWithAccessingMedicine { get; set; }

        [Column("help_with_accessing_other_essentials")]
        public bool? HelpWithAccessingOtherEssentials { get; set; }

        [Column("help_with_debt_and_money")] public bool? HelpWithDebtAndMoney { get; set; }

        [Column("help_with_health")] public bool? HelpWithHealth { get; set; }

        [Column("help_with_mental_health")] public bool? HelpWithMentalHealth { get; set; }

        [Column("help_with_accessing_internet")]
        public bool? HelpWithAccessingInternet { get; set; }

        [Column("help_with_something_else")] public bool? HelpWithSomethingElse { get; set; }

        [Column("help_with_housing")] public bool? HelpWithHousing { get; set; }

        [Column("help_with_jobs_or_training")] public bool? HelpWithJobsOrTraining { get; set; }

        [Column("help_with_children_and_schools")]
        public bool? HelpWithChildrenAndSchools { get; set; }

        [Column("help_with_disabilities")] public bool? HelpWithDisabilities { get; set; }

        [Column("medicine_delivery_help_needed")]
        public bool? MedicineDeliveryHelpNeeded { get; set; }

        [Column("when_is_medicines_delivered")]
        public string WhenIsMedicinesDelivered { get; set; }

        [Column("urgent_essentials")] public string UrgentEssentials { get; set; }

        [Column("urgent_essentials_anything_else")]
        public string UrgentEssentialsAnythingElse { get; set; }

        [Column("current_support")] public string CurrentSupport { get; set; }

        [Column("current_support_feedback")] public string CurrentSupportFeedback { get; set; }

        [Column("date_time_recorded")] public DateTime? DateTimeRecorded { get; set; }

        [Column("callback_required")] public bool? CallbackRequired { get; set; }

        [Column("initial_callback_completed")] public bool? InitialCallbackCompleted { get; set; }

        [Column("advice_notes")] public string AdviceNotes { get; set; }

        [Column("help_needed")] public string HelpNeeded { get; set; }

        [Column("help_needed_subtype")] public string HelpNeededSubtype { get; set; }

        [Column("nhs_ctas_id")] public string NhsCtasId { get; set; }

        [Column("assigned_staff")] public string AssignedTo { get; set; }

        [Column("metadata", TypeName = "jsonb")] public dynamic Metadata { get; set; }

        public ResidentEntity ResidentEntity { get; set; }
        public CallHandlerEntity CallHandlerEntity { get; set; }
        public List<CaseNoteEntity> CaseNotes { get; set; }
        public List<HelpRequestCallEntity> HelpRequestCalls { get; set; }

    }
}
