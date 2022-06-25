using External.ThirdParty.Services;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Domain.Services;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Domain
{
    public class ServiceConfigurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPriceCalculationService, PriceCalculationService>();
            services.AddScoped<IUnreliableServiceWrapper, UnreliableServiceWrapper>();
            services.AddScoped<IJobStatusValidator, JobStatusValidator>();
            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<ITranslatorStatusValidator, TranslatorStatusValidator>();
            services.AddScoped<ITranslatorService, TranslatorService>();

            // External
            services.AddScoped<INotificationService, UnreliableNotificationService>();
        }
    }
}
