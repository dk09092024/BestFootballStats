using MediatR;

namespace Domain.Features.Players.Add;

public record struct AddPlayerRequest(string Name,Enum Position) : IRequest<AddPlayerResult>;