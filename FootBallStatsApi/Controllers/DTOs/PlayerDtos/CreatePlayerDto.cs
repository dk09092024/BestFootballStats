namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct CreatePlayerDto
{
    public string Name { get; set; }
    public Enum Position { get; set; }
}