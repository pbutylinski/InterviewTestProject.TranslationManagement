using Microsoft.EntityFrameworkCore;
using TranslationManagement.DataAccess.Models;

namespace TranslationManagement.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<TranslationJob> TranslationJobs { get; set; }

        public DbSet<TranslatorModel> Translators { get; set; }
    }
}