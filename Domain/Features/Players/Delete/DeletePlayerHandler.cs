using Domain.Repositories;
using MediatR;

namespace Domain.Features.Players.Delete;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerRequest>
{
    private readonly IPlayerRepository _playerRepository;

    public DeletePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task Handle(DeletePlayerRequest request, CancellationToken cancellationToken)
    {
        await _playerRepository.DeleteAsync(request.Id,cancellationToken);
    }
}