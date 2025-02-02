namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct LinkTeamDto
{
    public required Guid LeagueId { get; set; }
    public required Guid TeamId { get; set; }
}