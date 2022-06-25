using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Domain.Services;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Api.Commands
{
    public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, bool>
    {
        private readonly ITranslationJobService translationJobService;
        private readonly IJobStatusValidator validator;

        public UpdateJobStatusCommandHandler(
            ITranslationJobService translationJobService,
            IJobStatusValidator validator)
        {
            this.translationJobService = translationJobService;
            this.validator = validator;
        }

        public async Task<bool> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            var model = await this.translationJobService.Get(request.JobId);
            if (model == null) return false;

            this.validator.ValidateAndThrow(model.Status, request.NewStatus);

            return await this.translationJobService.UpdateStatus(request.JobId, request.NewStatus);
        }
    }
}
