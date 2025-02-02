namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct LeagueCreatedDto
{
    public Guid Id { get; set; }
    public Uri LeagueUri => new Uri($"/api/league/{Id}", UriKind.Relative);
}