namespace cv19ResSupportV3.V4.Helpers
{
    public static class Predicates
    {
        public static bool IsNotNullAndNotEmpty(string testString)
        {
            return testString != null && testString.Trim() != "";
        }
    }
}
