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
            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<ITranslatorService, TranslatorService>();

            // Validators
            services.AddScoped<IJobStatusValidator, JobStatusValidator>();
            services.AddScoped<ITranslatorStatusValidator, TranslatorStatusValidator>();
            services.AddScoped<ICertifiedTranslatorValidator, CertifiedTranslatorValidator>();

            // External
            services.AddScoped<INotificationService, UnreliableNotificationService>();
        }
    }
}
