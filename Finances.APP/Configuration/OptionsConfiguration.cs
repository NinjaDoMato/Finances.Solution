using Finances.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Configuration;

namespace Finances.APP.Configuration;

public static class OptionsConfiguration
{
    public static IServiceCollection ResolveOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetSection("DefaultConnection");
        services.Configure<DbContextOptions<DatabaseContext>>(connString);

        return services;
    }
}
