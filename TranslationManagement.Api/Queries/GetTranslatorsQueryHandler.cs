using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Queries
{
    public class GetTranslatorsQueryHandler : IRequestHandler<GetTranslatorsQuery, GetTranslatorsQueryResult[]>
    {
        private readonly ITranslatorService translatorService;
        private readonly IMapper mapper;

        public GetTranslatorsQueryHandler(
            ITranslatorService translatorService,
            IMapper mapper)
        {
            this.translatorService = translatorService ?? throw new ArgumentNullException(nameof(translatorService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<GetTranslatorsQueryResult[]> Handle(GetTranslatorsQuery request, CancellationToken cancellationToken)
        {
            var items = this.translatorService
                .GetAll()
                .Select(this.mapper.Map<GetTranslatorsQueryResult>)
                .ToArray();
            
            return Task.FromResult(items);
        }
    }
}
