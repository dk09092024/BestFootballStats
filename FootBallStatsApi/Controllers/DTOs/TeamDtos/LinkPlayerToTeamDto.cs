namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct LinkPlayerToTeamDto
{
    public required Guid TeamId { get; set; }
    public required Guid PlayerId { get; set; }
}