using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct PlayerCreatedDto
{
    public required Guid Id { get; set; }
    public Link[] Links => new Link[] { new Link($"/api/player/{Id}", "player", "GET") };
}