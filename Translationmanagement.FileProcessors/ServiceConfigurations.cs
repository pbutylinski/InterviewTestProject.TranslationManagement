using Microsoft.Extensions.DependencyInjection;

namespace TranslationManagement.FileProcessors
{
    public static class ServiceConfigurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileProcessor, FileProcessor>();
        }
    }
}
