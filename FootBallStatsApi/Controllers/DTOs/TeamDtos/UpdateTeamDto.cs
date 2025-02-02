namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct UpdateTeamDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}