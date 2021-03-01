namespace cv19ResSupportV3.V4.Boundary.Requests
{
    public class CreateCaseNoteRequest
    {
        /// <summary>
        /// Case note content
        /// </summary>
        /// <example>{"note": "content","author":"name", "noteDate": "2020-02-02", "helpNeeded": "Shielding"}</example>
        public string CaseNote { get; set; }
    }
}
