using Domain.Features.Players.Delete;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Players.Delete;
[TestFixture]
public class DeletePlayerHandlerTests
{
    private DeletePlayerHandler _handler;
    private Mock<IPlayerRepository> _playerRepository;
    
    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _handler = new DeletePlayerHandler(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Delete_Player()
    {
        var playerId = Guid.NewGuid();
        var request = new DeletePlayerRequest(playerId);
        _playerRepository.Setup(x => x.DeleteAsync(playerId, CancellationToken.None));
        await _handler.Handle(request, CancellationToken.None);
        _playerRepository.Verify(x => x.DeleteAsync(playerId, CancellationToken.None), Times.Once);
    }
    
}