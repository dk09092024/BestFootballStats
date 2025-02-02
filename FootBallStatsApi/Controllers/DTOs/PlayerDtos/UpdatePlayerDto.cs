namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct UpdatePlayerDto
{
    public Guid id;
    public string Name { get; set; }
    public int Position { get; set; }
}