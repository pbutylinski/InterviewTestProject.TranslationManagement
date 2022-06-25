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

        public async Task<TranslationJob?> Get(int id)
        {
            return await this.dbContext.TranslationJobs.FindAsync(id);
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

        public async Task Update(int id, TranslationJob data)
        {
            this.dbContext.TranslationJobs.Update(data);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
