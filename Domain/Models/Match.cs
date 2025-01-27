using Domain.Models.Common;

namespace Domain.Models;

public class Match : Entity
{
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
    
    public Team HomeTeam { get; set; } = null!;
    public Team AwayTeam { get; set; } = null!;

    public int TotalPasses { get; set; }
}