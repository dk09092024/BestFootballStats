namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct CreateLeagueDto
{
    public required string Name { get; set; }
}