using MediatR;

namespace Domain.Features.Teams.Add;

public record struct AddTeamRequest(string Name) : IRequest<AddTeamResponse>;