using AutoMapper;
using TranslationManagement.DataAccess;
using TranslationManagement.Domain.Models;
using TranslatorDb = TranslationManagement.DataAccess.Models.Translator;

namespace TranslationManagement.Domain.Services
{
    public interface ITranslatorService
    {
        Task<int> Create(Translator translator);

        Task<bool> UpdateStatus(int translatorId, string newStatus);

        Translator? Get(int translatorId);

        List<Translator> GetAll();

        Translator? Find(string name);
    }

    public class TranslatorService : ITranslatorService
    {
        private readonly IRepository<TranslatorDb> repository;
        private readonly IMapper mapper;

        public TranslatorService(
            IRepository<TranslatorDb> repository,
            IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Create(Translator translator)
        {
            var dbObject = this.mapper.Map<TranslatorDb>(translator);
            return await this.repository.Create(dbObject);
        }

        public Translator? Find(string name)
        {
            var dbObject = this.repository.FindByName(name);
            if (dbObject == null) return null;
            return this.mapper.Map<Translator>(dbObject);
        }

        public List<Translator> GetAll()
        {
            return this.repository
                .GetAll()
                .Select(this.mapper.Map<Translator>)
                .ToList();
        }

        public Translator? Get(int translatorId)
        {
            var dbObject = this.repository.Get(translatorId);
            if (dbObject == null) return null;
            return this.mapper.Map<Translator>(dbObject);
        }

        public async Task<bool> UpdateStatus(int translatorId, string newStatus)
        {
            var dbObject = this.repository.Get(translatorId);
            if (dbObject == null) return false;

            dbObject.Status = newStatus;
            await this.repository.Update(dbObject);

            return true;
        }
    }
}
