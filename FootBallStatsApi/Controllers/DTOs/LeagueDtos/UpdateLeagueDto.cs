namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct UpdateLeagueDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}