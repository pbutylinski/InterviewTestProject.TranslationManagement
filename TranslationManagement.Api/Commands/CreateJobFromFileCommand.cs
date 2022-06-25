using MediatR;
using System.IO;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobFromFileCommand : IRequest<int>
    {
        public string Customer { get; set; }

        public string FileName { get; set; }

        public Stream FileStream { get; set; }
    }
}
