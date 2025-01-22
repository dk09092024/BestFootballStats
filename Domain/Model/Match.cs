namespace Domain.Model;

public class Match
{
    public required Guid HomeTeamId { get; set; }
    public required Guid AwayTeamId { get; set; }
    
    public required Team HomeTeam { get; set; }
    public required Team AwayTeam { get; set; }
    
    public required int TotalPasses { get; set; }
}