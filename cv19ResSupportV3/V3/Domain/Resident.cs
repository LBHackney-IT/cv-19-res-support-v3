using System;
using System.Collections.Generic;

namespace cv19ResSupportV3.V3.Domain
{
    public class Resident
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public string Uprn { get; set; }
        public string Ward { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public bool? IsPharmacistAbleToDeliver { get; set; }
        public string NameAddressPharmacist { get; set; }
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
        public List<ResidentCaseNote> CaseNotes { get; set; }
        public string NhsNumber { get; set; }
    }
}
