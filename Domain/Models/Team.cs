using Domain.Models.Common;

namespace Domain.Models;

public class Team : Entity
{
    public required string Name { get; set; }
    public required Guid LeagueId { get; set; }
    public required List<Player> Players { get; set; }
}