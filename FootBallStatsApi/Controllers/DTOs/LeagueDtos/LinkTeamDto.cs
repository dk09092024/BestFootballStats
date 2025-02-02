namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct LinkTeamDto
{
    public Guid LeagueId { get; set; }
    public Guid TeamId { get; set; }
}