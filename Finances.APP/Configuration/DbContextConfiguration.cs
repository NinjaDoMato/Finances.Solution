using Finances.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Finances.APP.Configuration
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DatabaseContext>(options =>
            options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

            return services;
        }
    }
}
