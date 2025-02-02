namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct GetPlayerDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Enum Position { get; set; }
    public Guid? TeamId { get; set; }
    public Uri? TeamUri => TeamId.HasValue ? new Uri($"/api/team/{TeamId}", UriKind.Relative) : null;
}