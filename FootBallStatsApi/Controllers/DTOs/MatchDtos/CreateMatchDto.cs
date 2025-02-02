namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct CreateMatchDto
{
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
}