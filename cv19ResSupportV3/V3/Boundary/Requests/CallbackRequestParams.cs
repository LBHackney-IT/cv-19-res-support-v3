namespace cv19ResSupportV3.V3.Boundary.Requests
{
    public class CallbackRequestParams
    {
        /// <summary>
        /// Are the required records master or duplicate
        /// </summary>
        /// <example>MASTER</example>
        public string Master { get; set; }
        /// <summary>
        /// The type of help request
        /// </summary>
        /// <example>Contact Tracing</example>
        public string HelpNeeded { get; set; }
    }
}
