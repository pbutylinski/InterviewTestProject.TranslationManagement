using TranslationManagement.DataAccess.Models;

namespace TranslationManagement.DataAccess.Repositories
{
    public class TranslatorRepository : IRepository<Translator>
    {
        private readonly AppDbContext dbContext;

        public TranslatorRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Translator?> Get(int id)
        {
            return await this.dbContext.Translators.FindAsync(id);
        }

        public async Task<int> Create(Translator data)
        {
            this.dbContext.Translators.Add(data);
            await this.dbContext.SaveChangesAsync();
            return data.Id;
        }

        public async Task Delete(int id)
        {
            var dbModel = await this.dbContext.Translators.FindAsync(id);

            if (dbModel != null)
            {
                this.dbContext.Translators.Remove(dbModel);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(int id, Translator data)
        {
            this.dbContext.Translators.Update(data);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
