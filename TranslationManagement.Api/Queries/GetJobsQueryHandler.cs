using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Queries
{
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, GetJobsQueryResult[]>
    {
        private readonly ITranslationJobService translationJobService;
        private readonly IMapper mapper;

        public GetJobsQueryHandler(
            ITranslationJobService translationJobService,
            IMapper mapper)
        {
            this.translationJobService = translationJobService ?? throw new ArgumentNullException(nameof(translationJobService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<GetJobsQueryResult[]> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            var results = this.translationJobService.GetAll()
                .Select(this.mapper.Map<GetJobsQueryResult>)
                .ToArray();

            return Task.FromResult(results);
        }
    }
}
