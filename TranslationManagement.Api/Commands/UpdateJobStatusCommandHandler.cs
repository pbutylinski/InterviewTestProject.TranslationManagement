using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.DataAccess;
using TranslationManagement.DataAccess.Models;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Api.Commands
{
    public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, bool>
    {
        private readonly IRepository<TranslationJob> repository;
        private readonly IJobStatusValidator validator;

        public UpdateJobStatusCommandHandler(
            IRepository<TranslationJob> repository,
            IJobStatusValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<bool> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            var currentJob = await this.repository.Get(request.JobId);
            if (currentJob == null) { return false; }

            this.validator.ValidateAndThrow(currentJob.Status, request.NewStatus);
            currentJob.Status = request.NewStatus;
            await this.repository.Update(currentJob);

            return true;
        }
    }
}
