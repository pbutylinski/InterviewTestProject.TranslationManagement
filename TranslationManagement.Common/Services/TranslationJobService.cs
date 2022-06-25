using AutoMapper;
using TranslationManagement.Common.Constants;
using TranslationManagement.DataAccess;
using TranslationManagement.Domain.Models;
using TranslationJobDb = TranslationManagement.DataAccess.Models.TranslationJob;

namespace TranslationManagement.Domain.Services
{
    public interface ITranslationJobService
    {
        Task<int> Create(TranslationJob job);

        Task<bool> UpdateStatus(int jobId, string newStatus, int translatorId);

        TranslationJob Get(int jobId);

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
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.priceCalculation = priceCalculation ?? throw new ArgumentNullException(nameof(priceCalculation));
            this.serviceWrapper = serviceWrapper ?? throw new ArgumentNullException(nameof(serviceWrapper));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public TranslationJob Get(int jobId)
        {
            var job = this.repository.Get(jobId);
            return this.mapper.Map<TranslationJob>(job);
        }

        public async Task<bool> UpdateStatus(int jobId, string newStatus, int translatorId)
        {
            var currentJob = this.repository.Get(jobId);
            if (currentJob == null) return false;

            currentJob.Status = newStatus;
            currentJob.AssignedTranslatorId = translatorId;

            await this.repository.Update(currentJob);

            return true;
        }

        public List<TranslationJob> GetAll()
        {
            var items = this.repository.GetAll();

            return items
                .Select(this.mapper.Map<TranslationJob>)
                .ToList();
        }
    }
}
