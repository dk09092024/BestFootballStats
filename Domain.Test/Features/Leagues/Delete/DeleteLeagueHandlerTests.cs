using Domain.Features.Leagues.Delete;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Leagues.Delete;
[TestFixture]
public class DeleteLeagueHandlerTests
{
    private DeleteLeagueHandler _handler;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _handler = new DeleteLeagueHandler(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Delete_League()
    {
        var command = new DeleteLeagueRequest { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.DeleteAsync(command.Id, CancellationToken.None));
        
        await _handler.Handle(command, CancellationToken.None);
        
        _leagueRepository.Verify(x=>x.DeleteAsync(command.Id, CancellationToken.None), Times.Once);
    }
}