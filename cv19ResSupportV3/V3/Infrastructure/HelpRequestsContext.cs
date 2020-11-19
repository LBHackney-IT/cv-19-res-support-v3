using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace cv19ResSupportV3.V3.Infrastructure
{
    public class HelpRequestsContext : DbContext
    {
        public HelpRequestsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<HelpRequestEntity> HelpRequestEntities { get; set; }
        public DbSet<LookupEntity> Lookups { get; set; }
        public DbSet<HelpRequestCallsEntity> HelpRequestCallsEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelpRequestEntity>(entity =>
                    {
                        entity.ToTable("i_need_help_resident_support_v3");
                        entity.HasKey(helpRequest => new {helpRequest.Id});
                        entity.Property(e => e.Id).HasColumnName("id");
                        entity.Property(e => e.IsOnBehalf)
                            .HasColumnName("is_on_behalf")
                            .HasColumnType("bool");
                        entity.Property(e => e.ConsentToCompleteOnBehalf)
                            .HasColumnName("consent_to_complete_on_behalf")
                            .HasColumnType("bool");;
                        entity.Property(e => e.OnBehalfFirstName)
                            .HasColumnName("on_behalf_first_name")
                            .HasColumnType("character varying");
                        entity.Property(e => e.OnBehalfLastName)
                            .HasColumnName("on_behalf_last_name")
                            .HasColumnType("character varying");
                        entity.Property(e => e.OnBehalfEmailAddress)
                            .HasColumnName("on_behalf_email_address")
                            .HasColumnType("character varying");
                        entity.Property(e => e.OnBehalfContactNumber)
                            .HasColumnName("on_behalf_contact_number")
                            .HasColumnType("character varying");
                        entity.Property(e => e.RelationshipWithResident)
                            .HasColumnName("relationship_with_resident")
                            .HasColumnType("character varying");
                        entity.Property(e => e.PostCode)
                            .HasColumnName("postcode")
                            .HasColumnType("character varying");
                        entity.Property(e => e.Uprn)
                            .HasColumnName("uprn")
                            .HasColumnType("character varying");
                        entity.Property(e => e.Ward)
                            .HasColumnName("ward")
                            .HasColumnType("character varying");
                        entity.Property(e => e.AddressFirstLine)
                            .HasColumnName("address_first_line")
                            .HasColumnType("character varying");
                        entity.Property(e => e.AddressSecondLine)
                            .HasColumnName("address_second_line")
                            .HasColumnType("character varying");
                        entity.Property(e => e.AddressThirdLine)
                            .HasColumnName("address_third_line")
                            .HasColumnType("character varying");
                        entity.Property(e => e.GettingInTouchReason)
                            .HasColumnName("getting_in_touch_reason")
                            .HasColumnType("character varying");
                        entity.Property(e => e.HelpWithAccessingFood)
                            .HasColumnName("help_with_accessing_food")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithCompletingNssForm)
                            .HasColumnName("help_with_completing_nss_form")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithAccessingSupermarketFood)
                            .HasColumnName("help_with_accessing_supermarket_food")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithShieldingGuidance)
                            .HasColumnName("help_with_shielding_guidance")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithNoNeedsIdentified)
                            .HasColumnName("help_with_no_needs_identified")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithAccessingMedicine)
                            .HasColumnName("help_with_accessing_medicine")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithAccessingOtherEssentials)
                            .HasColumnName("help_with_accessing_other_essentials")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithDebtAndMoney)
                            .HasColumnName("help_with_debt_and_money")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithHealth)
                            .HasColumnName("help_with_health")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithMentalHealth)
                            .HasColumnName("help_with_mental_health")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithAccessingInternet)
                            .HasColumnName("help_with_accessing_internet")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithSomethingElse)
                            .HasColumnName("help_with_something_else")
                            .HasColumnType("bool");
                        entity.Property(e => e.MedicineDeliveryHelpNeeded)
                            .HasColumnName("medicine_delivery_help_needed")
                            .HasColumnType("bool");
                        entity.Property(e => e.IsPharmacistAbleToDeliver)
                            .HasColumnName("is_pharmacist_able_to_deliver")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithHousing)
                            .HasColumnName("help_with_housing")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithJobsOrTraining)
                            .HasColumnName("help_with_jobs_or_training")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithChildrenAndSchools)
                            .HasColumnName("help_with_children_and_schools")
                            .HasColumnType("bool");
                        entity.Property(e => e.HelpWithDisabilities)
                            .HasColumnName("help_with_disabilities")
                            .HasColumnType("bool");
                        entity.Property(e => e.WhenIsMedicinesDelivered)
                            .HasColumnName("when_is_medicines_delivered")
                            .HasColumnType("character varying");
                        entity.Property(e => e.NameAddressPharmacist)
                            .HasColumnName("name_address_pharmacist")
                            .HasColumnType("character varying");
                        entity.Property(e => e.UrgentEssentials)
                            .HasColumnName("urgent_essentials")
                            .HasColumnType("character varying");
                        entity.Property(e => e.UrgentEssentialsAnythingElse)
                            .HasColumnName("urgent_essentials_anything_else")
                            .HasColumnType("character varying");
                        entity.Property(e => e.CurrentSupport)
                            .HasColumnName("current_support")
                            .HasColumnType("character varying");
                        entity.Property(e => e.CurrentSupportFeedback)
                            .HasColumnName("current_support_feedback")
                            .HasColumnType("character varying");
                        entity.Property(e => e.FirstName)
                            .HasColumnName("first_name")
                            .HasColumnType("character varying");
                        entity.Property(e => e.LastName)
                            .HasColumnName("last_name")
                            .HasColumnType("character varying");
                        entity.Property(e => e.DobMonth)
                            .HasColumnName("dob_month")
                            .HasColumnType("character varying");
                        entity.Property(e => e.DobYear)
                            .HasColumnName("dob_year")
                            .HasColumnType("character varying");
                        entity.Property(e => e.DobDay)
                            .HasColumnName("dob_day")
                            .HasColumnType("character varying");
                        entity.Property(e => e.ContactTelephoneNumber)
                            .HasColumnName("contact_telephone_number")
                            .HasColumnType("character varying");
                        entity.Property(e => e.ContactMobileNumber)
                            .HasColumnName("contact_mobile_number")
                            .HasColumnType("character varying");
                        entity.Property(e => e.EmailAddress)
                            .HasColumnName("email_address")
                            .HasColumnType("character varying");
                        entity.Property(e => e.GpSurgeryDetails)
                            .HasColumnName("gp_surgery_details")
                            .HasColumnType("character varying");
                        entity.Property(e => e.NumberOfChildrenUnder18)
                            .HasColumnName("number_of_children_under_18")
                            .HasColumnType("character varying");
                        entity.Property(e => e.ConsentToShare)
                            .HasColumnName("consent_to_share")
                            .HasColumnType("bool");
                        entity.Property(e => e.DateTimeRecorded)
                            .HasColumnName("date_time_recorded");
                        entity.Property(e => e.RecordStatus)
                            .HasColumnName("record_status")
                            .HasColumnType("character varying");
                        entity.Property(e => e.InitialCallbackCompleted)
                            .HasColumnName("initial_callback_completed")
                            .HasColumnType("bool");
                        entity.Property(e => e.CallbackRequired)
                            .HasColumnName("callback_required")
                            .HasColumnType("bool");
                        entity.Property(e => e.CaseNotes)
                            .HasColumnName("case_notes")
                            .HasColumnType("character varying");
                        entity.Property(e => e.AdviceNotes)
                            .HasColumnName("advice_notes")
                            .HasColumnType("character varying");
                        entity.Property(e => e.HelpNeeded)
                            .HasColumnName("help_needed")
                            .HasColumnType("character varying");
                    }
                );

                        modelBuilder.Entity<LookupEntity>(entity =>
                    {
                        entity.ToTable("inh_lookups");
                        entity.HasKey(lookup => new {lookup.Id});
                        entity.Property(e => e.Id).HasColumnName("id");
                        entity.Property(e => e.LookupGroup)
                            .HasColumnName("lookup_group")
                            .HasColumnType("character varying");
                        entity.Property(e => e.Lookup)
                            .HasColumnName("lookup")
                            .HasColumnType("character varying");;
                    }
                );
                        modelBuilder.Entity<HelpRequestCallsEntity>(entity =>
                            {
                                entity.ToTable("help_request_calls");
                                entity.HasKey(call => new {call.Id});
                                entity.Property(e => e.Id).HasColumnName("id");
                                entity.Property(e => e.HelpRequestId).HasColumnName("help_request_id");
                                entity.Property(e => e.CallType)
                                    .HasColumnName("call_type")
                                    .HasColumnType("character varying");
                                entity.Property(e => e.CallOutcome)
                                    .HasColumnName("call_outcome")
                                    .HasColumnType("character varying");
                                entity.Property(e => e.CallDateTime)
                                    .HasColumnName("call_date_time");
                            }
                        );
        }
    }
}
