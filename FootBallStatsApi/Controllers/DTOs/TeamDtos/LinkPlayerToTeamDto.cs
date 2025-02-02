namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct LinkPlayerToTeamDto
{
    public Guid TeamId { get; set; }
    public Guid PlayerId { get; set; }
}