using Domain.Models.Enum;
using MediatR;

namespace Domain.Features.Players.Update;

public record struct UpdatePlayerRepository(Guid Id, string Name, Position Position) : IRequest;