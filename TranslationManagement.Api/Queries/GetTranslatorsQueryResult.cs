namespace TranslationManagement.Api.Queries
{
    public class GetTranslatorsQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public string Status { get; set; }
    }
}
