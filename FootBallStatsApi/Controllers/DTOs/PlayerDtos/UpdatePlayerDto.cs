using Domain.Models.Enum;

namespace FootBallStatsApi.Controllers.DTOs.PlayerDtos;

public record struct UpdatePlayerDto
{
    public required Guid id;
    public required string Name { get; set; }
    public required Position Position { get; set; }
}