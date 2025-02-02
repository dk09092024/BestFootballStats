namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct TeamCreatedDto
{
    public required Guid Id { get; set; }
    public Uri TeamUri => new Uri($"/api/team/{Id}", UriKind.Relative);
}