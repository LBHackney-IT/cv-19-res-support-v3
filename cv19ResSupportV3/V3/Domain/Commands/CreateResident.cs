using System.Collections.Generic;

namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class CreateResident
    {
        public string Postcode { get; set; }
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
        public string NhsNumber { get; set; }
        // NhsCtasId is used for finding a duplicate record;
        // it's not being stored on resident entity.
        public string NhsCtasId { get; set; }
    }
}
