using MediatR;

namespace TranslationManagement.Api.Commands
{
    public class CreateTranslatorCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public string CreditCardNumber { get; set; }
    }
}
