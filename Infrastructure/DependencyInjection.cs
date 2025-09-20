using TaxCalculator.Repositories;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTaxCalculatorServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register connection string to MDF file
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            // Calculation service
            services.AddSingleton<ITaxCalculator, ProgressiveTaxCalculator>();

            // Register repository for tax bands
            services.AddTransient<ITaxBandsRepository, TaxBandsRepository>();

            return services;
        }
    }
}