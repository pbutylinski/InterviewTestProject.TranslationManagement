namespace TranslationManagement.DataAccess.Models
{
    public class TranslatorModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Status { get; set; }
        public string? CreditCardNumber { get; set; } // This should be in a separate, secure DB
    }
}
