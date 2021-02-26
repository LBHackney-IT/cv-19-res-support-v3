namespace cv19ResSupportV3.V3.Boundary.Requests
{
    public class RequestQueryParams
    {
        /// <summary>
        /// Postcode of the resident
        /// </summary>
        /// <example>A1 2BC</example>
        public string Postcode { get; set; }
        /// <summary>
        /// First name of the resident
        /// </summary>
        /// <example>Wednesday</example>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the resident
        /// </summary>
        /// <example>Adams</example>
        public string LastName { get; set; }
        /// <summary>
        /// Type of the help request
        /// </summary>
        /// <example>Contact Tracing</example>
        public string HelpNeeded { get; set; }
    }
}
