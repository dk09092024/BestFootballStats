using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ApiExtensions;

public static class ApplicationDatabaseExtension
{
    public static void AddApplicationDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(builder => 
            builder.UseSqlite("DataSource=file:../Infrastructure/Database/Data/database.db"));
    }
}