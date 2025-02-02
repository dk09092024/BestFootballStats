using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ApiExtensions;

public static class ApplicationDatabaseExtension
{
    public static void AddApplicationDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>();
    }
}