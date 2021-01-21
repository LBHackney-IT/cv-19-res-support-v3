namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class SearchRequest
    {
        public string Postcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HelpNeeded { get; set; }
    }
}
