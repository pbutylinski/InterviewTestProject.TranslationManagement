using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobCommand : IRequest<int>
    {
        [JsonIgnore]
        public string CustomerName { get; set; }

        public string OriginalContent { get; set; }
    }
}
