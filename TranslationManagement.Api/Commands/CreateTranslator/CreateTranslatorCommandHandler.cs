using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Models;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Commands
{
    public class CreateTranslatorCommandHandler : IRequestHandler<CreateTranslatorCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ITranslatorService translatorService;

        public CreateTranslatorCommandHandler(
            IMapper mapper,
            ITranslatorService translatorService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.translatorService = translatorService ?? throw new ArgumentNullException(nameof(translatorService));
        }

        public async Task<int> Handle(CreateTranslatorCommand request, CancellationToken cancellationToken)
        {
            var translator = this.mapper.Map<Translator>(request);
            return await this.translatorService.Create(translator);
        }
    }
}
