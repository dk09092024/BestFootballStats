namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct CreateTeamDto
{
    public required string Name { get; set; }
}