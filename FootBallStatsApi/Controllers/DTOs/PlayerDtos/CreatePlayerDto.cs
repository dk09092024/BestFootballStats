using Domain.Models.Enum;

namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct CreatePlayerDto
{
    public string Name { get; set; }
    public Position Position { get; set; }
}