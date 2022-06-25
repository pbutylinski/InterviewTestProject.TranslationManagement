using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Api.Commands
{
    public class UpdateTranslatorStatusCommandHandler : IRequestHandler<UpdateTranslatorStatusCommand, bool>
    {
        private readonly ITranslatorService translatorService;
        private readonly ITranslatorStatusValidator statusValidator;

        public UpdateTranslatorStatusCommandHandler(
            ITranslatorService translatorService, 
            ITranslatorStatusValidator statusValidator)
        {
            this.translatorService = translatorService ?? throw new System.ArgumentNullException(nameof(translatorService));
            this.statusValidator = statusValidator ?? throw new System.ArgumentNullException(nameof(statusValidator));
        }

        public async Task<bool> Handle(UpdateTranslatorStatusCommand request, CancellationToken cancellationToken)
        {
            this.statusValidator.ValidateAndThrow(request.NewStatus);
            return await this.translatorService.UpdateStatus(request.TranslatorId, request.NewStatus);
        }
    }
}
