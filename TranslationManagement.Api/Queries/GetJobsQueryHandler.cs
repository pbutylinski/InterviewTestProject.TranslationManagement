using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.DataAccess;
using TranslationManagement.DataAccess.Models;
using System;
using AutoMapper;
using System.Linq;

namespace TranslationManagement.Api.Queries
{
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, GetJobsQueryResult[]>
    {
        private readonly IRepository<TranslationJob> repository;
        private readonly IMapper mapper;

        public GetJobsQueryHandler(
            IRepository<TranslationJob> repository,
            IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<GetJobsQueryResult[]> Handle(GetJobsQuery request,
            CancellationToken cancellationToken)
        {
            var results = this.repository
                .GetAll()
                .Select(this.mapper.Map<GetJobsQueryResult>)
                .ToArray();

            return Task.FromResult(results);
        }
    }
}
