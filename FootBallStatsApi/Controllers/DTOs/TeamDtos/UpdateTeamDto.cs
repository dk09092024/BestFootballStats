namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct UpdateTeamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}