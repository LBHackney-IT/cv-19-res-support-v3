namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class FindResident
    {
        public string Uprn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }
        public string DobDay { get; set; }
    }
}
