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
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
            });

            return services;
        }
    }
}
