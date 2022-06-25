using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TranslationManagement.Common.Constants;
using TranslationManagement.DataAccess;
using TranslationManagement.DataAccess.Models;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Api.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IRepository<TranslationJob> repository;
        private readonly IPriceCalculationService priceCalculation;
        private readonly IUnreliableServiceWrapper serviceWrapper;
        private readonly IMapper mapper;

        public CreateJobCommandHandler(
            IRepository<TranslationJob> repository,
            IPriceCalculationService priceCalculation,
            IUnreliableServiceWrapper serviceWrapper,
            IMapper mapper)
        {
            this.repository = repository;
            this.priceCalculation = priceCalculation;
            this.serviceWrapper = serviceWrapper;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var domainObject = this.mapper.Map<Domain.TranslationJob>(request);
            domainObject.Status = JobStatus.New;

            this.priceCalculation.UpdatePrice(domainObject);

            var dbObject = this.mapper.Map<TranslationJob>(domainObject);
            var id = await this.repository.Create(dbObject);

            if (id > 0)
            {
                var notificationResult = await this.serviceWrapper.TrySendNewJobNotification(id);
                // TODO: log result
            }

            return id;
        }
    }
}
