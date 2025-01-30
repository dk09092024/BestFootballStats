using Domain.Features.Players.Add;
using Domain.Models;
using Domain.Models.Enum;
using Domain.Repositories;
using MediatR;
using Moq;

namespace Domain.Test.Features.Players.Add;
[TestFixture]
public class AddPlayerHandlerTests
{
    private AddPlayerHandler _handler;
    private Mock<IPlayerRepository> _playerRepository;

    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _handler = new AddPlayerHandler(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Add_Player()
    {
        var request = new AddPlayerRequest() { Name = "Test", Position = Position.Goalkeeper };
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.AddAsync(It.IsAny<Player>(), CancellationToken.None)).ReturnsAsync(playerId);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        _playerRepository.Verify(x => x.AddAsync(It.IsAny<Player>(), CancellationToken.None), Times.Once);
        Assert.That(result.Id, Is.EqualTo(playerId));
    }
}