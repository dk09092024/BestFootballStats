using MediatR;

namespace Domain.Features.Teams.Delete;

public record struct DeleteTeamRequest(Guid Id) : IRequest;