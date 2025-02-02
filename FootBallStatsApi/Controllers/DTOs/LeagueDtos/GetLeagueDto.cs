namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct GetLeagueDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid[] TeamIds { get; set; }
    public Uri[] TeamUris => TeamIds.Select(id => new Uri($"/api/team/{id}", UriKind.Relative)).ToArray();
}