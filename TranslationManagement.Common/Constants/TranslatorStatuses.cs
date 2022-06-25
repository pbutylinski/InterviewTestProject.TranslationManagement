namespace TranslationManagement.Domain.Constants
{
    public static class TranslatorStatus
    {
        public const string Applicant = "Applicant";
        public const string Certified = "Certified";
        public const string Deleted = "Deleted";

        public static string[] All => new[] { "Applicant", "Certified", "Deleted" };
    }
}
