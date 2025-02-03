using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct GetPlayerDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Enum Position { get; set; }
    public Guid? TeamId { get; set; }
    public Link[] Links => new Link[] { new Link($"/api/player/{Id}", "player", "GET") };
}