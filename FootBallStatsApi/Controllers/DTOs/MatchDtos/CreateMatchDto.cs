namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct CreateMatchDto
{
    public Guid HomeTeamId { get; set; }
    public Guid AwayTeamId { get; set; }
}