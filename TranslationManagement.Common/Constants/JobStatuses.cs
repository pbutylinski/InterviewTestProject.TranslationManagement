namespace TranslationManagement.Common.Constants
{
    public static class JobStatus
    {
        public const string New = "New";
        public const string InProgress = "InProgress";
        public const string Completed = "Completed";

        public static string[] All => new[] { New, InProgress, Completed };
    }
}
