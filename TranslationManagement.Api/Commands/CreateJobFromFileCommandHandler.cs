using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain;
using TranslationManagement.Domain.Services;
using TranslationManagement.FileProcessors;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobFromFileCommandHandler : IRequestHandler<CreateJobFromFileCommand, int>
    {
        private readonly ITranslationJobService translationJobService;
        private readonly IFileProcessor fileProcessor;

        public CreateJobFromFileCommandHandler(
            ITranslationJobService translationJobService,
            IFileProcessor fileProcessor)
        {
            this.translationJobService = translationJobService ?? throw new System.ArgumentNullException(nameof(translationJobService));
            this.fileProcessor = fileProcessor ?? throw new System.ArgumentNullException(nameof(fileProcessor));
        }

        public async Task<int> Handle(CreateJobFromFileCommand request, CancellationToken cancellationToken)
        {
            var result = this.fileProcessor.Process(request.FileName, request.FileStream);

            var job = new TranslationJob
            {
                CustomerName = request.Customer, // or result.Customer?
                OriginalContent = result.Content
            };

            return await this.translationJobService.Create(job);
        }
    }
}
