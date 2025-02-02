namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct PlayerCreatedDto
{
    public required Guid Id { get; set; }
    public Uri Uri => new Uri($"/api/player/{Id}", UriKind.Relative);
}