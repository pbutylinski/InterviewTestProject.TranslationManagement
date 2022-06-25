using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Api.Commands
{
    public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, bool>
    {
        private readonly ITranslationJobService translationJobService;
        private readonly ITranslatorService translatorService;
        private readonly IJobStatusValidator jobStatusValidator;
        private readonly ICertifiedTranslatorValidator certifiedTranslatorValidator;

        public UpdateJobStatusCommandHandler(
            ITranslationJobService translationJobService,
            ITranslatorService translatorService,
            IJobStatusValidator jobStatusValidator,
            ICertifiedTranslatorValidator certifiedTranslatorValidator)
        {
            this.translationJobService = translationJobService ?? throw new ArgumentNullException(nameof(translationJobService));
            this.translatorService = translatorService ?? throw new ArgumentNullException(nameof(translatorService));
            this.jobStatusValidator = jobStatusValidator ?? throw new ArgumentNullException(nameof(jobStatusValidator));
            this.certifiedTranslatorValidator = certifiedTranslatorValidator ?? throw new ArgumentNullException(nameof(certifiedTranslatorValidator));
        }

        public async Task<bool> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            var model = this.translationJobService.Get(request.JobId);
            var translator = this.translatorService.Get(request.TranslatorId);

            if (model == null || translator == null) return false;

            this.jobStatusValidator.ValidateAndThrow(model.Status, request.NewStatus);
            this.certifiedTranslatorValidator.ValidateAndThrow(request.NewStatus, translator.Status);

            return await this.translationJobService.UpdateStatus(request.JobId, request.NewStatus);
        }
    }
}
