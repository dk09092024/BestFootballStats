using FootBallStatsApi.Controllers.DTOs.Common;

namespace FootBallStatsApi.Controllers.DTOs.MatchDtos;

public record struct GetMatchDto
{
    public required Guid Id { get; set; }
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
    public required int TotalPasses { get; set; }
    public Link[] Links => new Link[]
    {
        new Link($"/api/team/{HomeTeamId}", "homeTeam", "GET"),
        new Link($"/api/team/{AwayTeamId}", "awayTeam", "GET"),
    };
}