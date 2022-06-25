namespace TranslationManagement.DataAccess.Models
{
    public class TranslationJob
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public string? OriginalContent { get; set; }
        public string? TranslatedContent { get; set; }
        public double Price { get; set; }
        public int? AssignedTranslatorId { get; set; }
        public Translator? AssignedTranslator { get; set; }
    }
}
