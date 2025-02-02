namespace FootBallStatsApi.Controllers.DTOs.TeamDtos;

public record struct GetTeamDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? LeagueId { get; set; }
    public Guid[] PlayerIds { get; set; }
    public Uri[] PlayerUris => PlayerIds.Select(id => new Uri($"/api/player/{id}", UriKind.Relative)).ToArray();
}