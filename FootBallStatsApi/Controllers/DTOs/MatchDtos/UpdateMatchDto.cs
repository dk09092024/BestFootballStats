namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct UpdateMatchDto
{
    public required Guid Id { get; set; }
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
}