using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FootBallStatsApi.ApiExtensions.DataSource;

public static class ApplicationDatabaseExtension
{
    public static void AddApplicationDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(builder => 
            builder.UseSqlite("DataSource=file:../Infrastructure/Database/Data/database.db"));
    }
}