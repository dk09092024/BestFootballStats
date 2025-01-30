using Domain.Features.Players.Get;
using Domain.Models;
using Domain.Models.Enum;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Players.Get;

public class GetPlayerHandlerTests
{
    private GetPlayerHandler _handler;
    private Mock<IPlayerRepository> _playerRepository;
    
    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _handler = new GetPlayerHandler(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Get_Player()
    {
        var playerId = Guid.NewGuid();
        var request = new GetPlayerQuery(playerId);
        var player = new Player
        {
            Id = playerId,
            Name = "Player 1",
            Position = Position.Goalkeeper,
            CreatedAt = DateTime.Now
        };
        _playerRepository.Setup(x => x.GetByIdAsync(playerId, CancellationToken.None)).ReturnsAsync(player);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        _playerRepository.Verify(x => x.GetByIdAsync(playerId, CancellationToken.None), Times.Once);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Player.Id, Is.EqualTo(player.Id));
        Assert.That(result.Player.Name, Is.EqualTo(player.Name));
        Assert.That(result.Player.Position, Is.EqualTo(player.Position));
        Assert.That(result.Player.CreatedAt, Is.EqualTo(player.CreatedAt));
    }
}