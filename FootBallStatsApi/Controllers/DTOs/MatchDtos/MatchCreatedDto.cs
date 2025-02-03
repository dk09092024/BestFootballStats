using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct MatchCreatedDto
{
    public required Guid Id { get; set; }
    public Link[] Links => new Link[] { new Link($"/api/match/{Id}", "match", "GET") };
}