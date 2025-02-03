using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct TeamCreatedDto
{
    public required Guid Id { get; set; }
    public Link[] Links => new Link[] { new Link($"/api/team/{Id}", "team", "GET") };
}