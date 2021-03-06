namespace TranslationManagement.Domain.Models
{
    /// <summary>
    /// This should be used internally across services
    /// </summary>
    public class TranslationJob
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public string? OriginalContent { get; set; }
        public string? TranslatedContent { get; set; }
        public double Price { get; set; }
        public Translator? AssignedTranslator { get; set; }
    }
}
