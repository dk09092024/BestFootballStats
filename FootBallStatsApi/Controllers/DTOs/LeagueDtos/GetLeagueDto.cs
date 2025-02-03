using FootBallStatsApi.Controllers.DTOs.Common;
using NuGet.Packaging;

namespace FootBallStatsApi.Controllers.DTOs.LeagueDtos;

public record struct GetLeagueDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid[] TeamIds { get; set; }
    public Link[] Links => TeamIds.Select(id => new Link($"/api/team/{id}", "team", "GET")).ToArray();
    
}