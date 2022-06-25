using MediatR;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobCommand : IRequest<int>
    {
        public string CustomerName { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public double Price { get; set; }
    }
}
