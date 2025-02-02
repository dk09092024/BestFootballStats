namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct UpdateMatchDto
{
    public Guid Id { get; set; }
    public Guid HomeTeamId { get; set; }
    public Guid AwayTeamId { get; set; }
}