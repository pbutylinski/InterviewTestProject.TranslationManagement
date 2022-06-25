using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.DataAccess.Models;
using TranslationManagement.DataAccess.Repositories;

namespace TranslationManagement.DataAccess
{
    public static class ServiceConfigurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite("Data Source=TranslationAppDatabase.db"));

            services.AddScoped<IRepository<TranslationJob>, TranslationJobRepository>();
            services.AddScoped<IRepository<Translator>, TranslatorRepository>();
        }
    }
}
