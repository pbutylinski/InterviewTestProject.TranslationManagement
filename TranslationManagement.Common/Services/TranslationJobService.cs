using AutoMapper;
using TranslationManagement.Common.Constants;
using TranslationManagement.DataAccess;
using TranslationJobDb = TranslationManagement.DataAccess.Models.TranslationJob;

namespace TranslationManagement.Domain.Services
{
    public interface ITranslationJobService
    {
        Task<int> Create(TranslationJob job);

        Task<bool> UpdateStatus(int jobId, string newStatus);

        Task<TranslationJob> Get(int jobId);

        List<TranslationJob> GetAll();
    }

    public class TranslationJobService : ITranslationJobService
    {
        private readonly IRepository<TranslationJobDb> repository;
        private readonly IPriceCalculationService priceCalculation;
        private readonly IUnreliableServiceWrapper serviceWrapper;
        private readonly IMapper mapper;

        public TranslationJobService(
            IRepository<TranslationJobDb> repository,
            IPriceCalculationService priceCalculation,
            IUnreliableServiceWrapper serviceWrapper,
            IMapper mapper)
        {
            this.repository = repository;
            this.priceCalculation = priceCalculation;
            this.serviceWrapper = serviceWrapper;
            this.mapper = mapper;
        }

        public async Task<int> Create(TranslationJob job)
        {
            job.Status = JobStatus.New;

            this.priceCalculation.UpdatePrice(job);

            var dbObject = this.mapper.Map<TranslationJobDb>(job);
            var id = await this.repository.Create(dbObject);

            if (id > 0)
            {
                var notificationResult = await this.serviceWrapper.TrySendNewJobNotification(id);
                // TODO: log result
            }

            return id;
        }

        public async Task<TranslationJob> Get(int jobId)
        {
            var job = await this.repository.Get(jobId);
            return this.mapper.Map<TranslationJob>(job);
        }

        public async Task<bool> UpdateStatus(int jobId, string newStatus)
        {
            var currentJob = await this.repository.Get(jobId);
            if (currentJob == null) return false;

            currentJob.Status = newStatus;
            await this.repository.Update(currentJob);

            return true;
        }

        public List<TranslationJob> GetAll()
        {
            return this.repository
                .GetAll()
                .Select(this.mapper.Map<TranslationJob>)
                .ToList();
        }
    }
}
