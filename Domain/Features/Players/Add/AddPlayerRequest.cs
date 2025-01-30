using Domain.Models.Enum;
using MediatR;

namespace Domain.Features.Players.Add;

public record struct AddPlayerRequest(string Name,Position Position) : IRequest<AddPlayerResult>;