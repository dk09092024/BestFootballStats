using Domain.Repositories;
using Infrastructure.Repositories;

namespace FootBallStatsApi.ApiExtensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ILeagueRepository, LeagueRepository>();
    }
}