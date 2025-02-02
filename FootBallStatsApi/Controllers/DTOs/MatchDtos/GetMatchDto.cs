namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct GetMatchDto
{
    public Guid Id { get; set; }
    public Guid HomeTeamId { get; set; }
    public Guid AwayTeamId { get; set; }
    public int TotalPasses { get; set; }

    public Uri HomeTeamUri => new Uri($"/api/team/{HomeTeamId}", UriKind.Relative);

    public Uri AwayTeamUri => new Uri($"/api/team/{AwayTeamId}", UriKind.Relative);
}