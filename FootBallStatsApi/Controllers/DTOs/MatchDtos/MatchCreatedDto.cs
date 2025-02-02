namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct MatchCreatedDto
{
    public Guid Id { get; set; }
    public Uri MatchUri => new Uri($"/api/match/{Id}", UriKind.Relative);
}