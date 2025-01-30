using Domain.Features.Players.Update;
using Domain.Models;
using Domain.Models.Enum;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Players.Update;
[TestFixture]
public class UpdatePlayerHandlerTests
{
    private UpdatePlayerHandler _handler;
    private Mock<IPlayerRepository> _playerRepository;
    
    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _handler = new UpdatePlayerHandler(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Update_Player()
    {
        var request = new UpdatePlayerRequest() { Id = Guid.NewGuid(), Name = "Player 1", Position = Position.Goalkeeper };
       var player = new Player
        {
            Id = request.Id,
            Name = request.Name,
            Position = request.Position,
            CreatedAt = DateTime.Now
        };
        _playerRepository.Setup(x => x.GetByIdAsync(request.Id, CancellationToken.None)).ReturnsAsync(player);
        _playerRepository.Setup(x => x.UpdateAsync(It.IsAny<Player>(),CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _playerRepository.Verify(x => x.GetByIdAsync(request.Id, CancellationToken.None), Times.Once);
        _playerRepository.Verify(x => x.UpdateAsync(It.IsAny<Player>(),CancellationToken.None), Times.Once);
    }
    
}