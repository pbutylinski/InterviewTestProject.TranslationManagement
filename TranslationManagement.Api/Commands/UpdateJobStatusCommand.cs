using MediatR;
using System.Text.Json.Serialization;

namespace TranslationManagement.Api.Commands
{
    public class UpdateJobStatusCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int JobId { get; set; }

        public int TranslatorId { get; set; }

        public string NewStatus { get; set; }
    }
}
