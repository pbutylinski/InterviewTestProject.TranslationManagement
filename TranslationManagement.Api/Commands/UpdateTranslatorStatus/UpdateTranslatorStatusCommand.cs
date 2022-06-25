using MediatR;
using System.Text.Json.Serialization;

namespace TranslationManagement.Api.Commands
{
    public class UpdateTranslatorStatusCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int TranslatorId { get; set; }

        public string NewStatus { get; set; }
    }
}
