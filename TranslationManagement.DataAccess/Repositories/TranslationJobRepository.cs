using Microsoft.EntityFrameworkCore;
using TranslationManagement.DataAccess.Models;

namespace TranslationManagement.DataAccess.Repositories
{
    public class TranslationJobRepository : IRepository<TranslationJob>
    {
        private readonly AppDbContext dbContext;

        public TranslationJobRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TranslationJob> GetAll()
        {
            return this.dbContext.TranslationJobs
                .Include(x => x.AssignedTranslator)
                .AsEnumerable();
        }

        public TranslationJob? Get(int id)
        {
            return this.dbContext.TranslationJobs
                .Include(x => x.AssignedTranslator)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> Create(TranslationJob data)
        {
            this.dbContext.TranslationJobs.Add(data);
            await this.dbContext.SaveChangesAsync();
            return data.Id;
        }

        public async Task Delete(int id)
        {
            var dbModel = await this.dbContext.TranslationJobs.FindAsync(id);

            if (dbModel != null)
            {
                this.dbContext.TranslationJobs.Remove(dbModel);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(TranslationJob data)
        {
            this.dbContext.TranslationJobs.Update(data);
            await this.dbContext.SaveChangesAsync();
        }

        public TranslationJob? FindByName(string name)
        {
            return this.dbContext.TranslationJobs
                .Include(x => x.AssignedTranslator)
                .FirstOrDefault(x => x.CustomerName == name);
        }
    }
}
