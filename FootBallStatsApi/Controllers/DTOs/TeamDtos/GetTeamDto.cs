using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct GetTeamDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? LeagueId { get; set; }
    public Guid[] PlayerIds { get; set; } 
    public Link[] Links => PlayerIds.Select(p=> new Link($"/api/player/{p}", "player", "GET")).ToArray();
}