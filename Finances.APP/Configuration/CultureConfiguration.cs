using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Finances.APP.Configuration
{
    public static class CultureConfiguration
    {
        public static IServiceCollection ConfigureCulture(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
            });

            return services;
        }
    }
}
