using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("residents")]
    public class ResidentEntity
    {
        public ResidentEntity()
        {
            HelpRequestsNew = new List<HelpRequestEntity>();
            CaseNotes = new List<CaseNoteEntity>();
        }
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("dob_day")]
        public string DobDay { get; set; }

        [Column("dob_month")]
        public string DobMonth { get; set; }

        [Column("dob_year")]
        public string DobYear { get; set; }

        [Column("contact_mobile_number")]
        public string ContactMobileNumber { get; set; }

        [Column("contact_telephone_number")]
        public string ContactTelephoneNumber { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Column("address_first_line")]
        public string AddressFirstLine { get; set; }

        [Column("address_second_line")]
        public string AddressSecondLine { get; set; }

        [Column("address_third_line")]
        public string AddressThirdLine { get; set; }

        [Column("postcode")]
        public string PostCode { get; set; }

        [Column("uprn")]
        public string Uprn { get; set; }

        [Column("ward")]
        public string Ward { get; set; }

        [Column("is_pharmacist_able_to_deliver")]
        public bool? IsPharmacistAbleToDeliver { get; set; }

        [Column("name_address_pharmacist")]
        public string NameAddressPharmacist { get; set; }

        [Column("gp_surgery_details")]
        public string GpSurgeryDetails { get; set; }

        [Column("number_of_children_under_18")]
        public string NumberOfChildrenUnder18 { get; set; }

        [Column("consent_to_share")]
        public bool? ConsentToShare { get; set; }

        [Column("record_status")]
        public string RecordStatus { get; set; }


        [Column("nhs_number")]
        public string NhsNumber { get; set; }

        public List<HelpRequestEntity> HelpRequestsNew { get; set; }
        public List<CaseNoteEntity> CaseNotes { get; set; }
    }
}
