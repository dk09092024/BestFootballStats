namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct GetMatchDto
{
    public required Guid Id { get; set; }
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
    public required int TotalPasses { get; set; }

    public Uri HomeTeamUri => new Uri($"/api/team/{HomeTeamId}", UriKind.Relative);

    public Uri AwayTeamUri => new Uri($"/api/team/{AwayTeamId}", UriKind.Relative);
}