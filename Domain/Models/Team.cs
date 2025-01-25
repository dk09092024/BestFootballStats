using Domain.Models.Common;

namespace Domain.Models;

public class Team : Entity
{
    public required string Name { get; set; }
    public Guid LeagueId { get; set; }
    public List<Player> Players { get; set; } = [];
}