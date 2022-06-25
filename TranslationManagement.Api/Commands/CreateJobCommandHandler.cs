using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ITranslationJobService translationJobService;

        public CreateJobCommandHandler(
            IMapper mapper,
            ITranslationJobService translationJobService)
        {
            this.mapper = mapper;
            this.translationJobService = translationJobService;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = this.mapper.Map<Domain.TranslationJob>(request);
            return await this.translationJobService.Create(job);
        }
    }
}
