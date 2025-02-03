using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct LeagueCreatedDto
{
    public required Guid Id { get; set; }
    public Link[] Links => new Link[] { new Link($"/api/league/{Id}", "league", "GET") };
    
}