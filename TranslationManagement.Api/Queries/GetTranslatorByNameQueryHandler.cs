using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Queries
{
    public class GetTranslatorByNameQueryHandler : IRequestHandler<GetTranslatorByNameQuery, GetTranslatorsQueryResult>
    {
        private readonly ITranslatorService translatorService;
        private readonly IMapper mapper;

        public GetTranslatorByNameQueryHandler(
            ITranslatorService translatorService,
            IMapper mapper)
        {
            this.translatorService = translatorService ?? throw new ArgumentNullException(nameof(translatorService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<GetTranslatorsQueryResult> Handle(GetTranslatorByNameQuery request, CancellationToken cancellationToken)
        {
            var item = this.translatorService.Find(request.Name);
            if (item == null) return Task.FromResult((GetTranslatorsQueryResult)null);
            return Task.FromResult(this.mapper.Map<GetTranslatorsQueryResult>(item));
        }
    }
}
