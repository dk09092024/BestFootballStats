using Domain.Models.Enum;

namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct CreatePlayerDto
{
    public required string Name { get; set; }
    public required Position Position { get; set; }
}