namespace TranslationManagement.Domain
{
    public class TranslationJob
    {
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public string? OriginalContent { get; set; }
        public string? TranslatedContent { get; set; }
        public double Price { get; set; }
    }
}
