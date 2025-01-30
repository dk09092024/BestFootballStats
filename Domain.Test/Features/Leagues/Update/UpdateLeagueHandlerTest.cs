using Domain.Features.Leagues.Update;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Leagues.Update;
[TestFixture]
public class UpdateLeagueHandlerTest
{
    private UpdateLeagueHandler _handler;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _handler = new UpdateLeagueHandler(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Update_League()
    {
        var request = new UpdateLeagueRequest { Id = Guid.NewGuid(), Name = "Test" };
        var league = new League
        {
            Id = request.Id,
            Name = request.Name,
            CreatedAt = DateTime.Now
        };
        _leagueRepository.Setup(x => x.GetByIdAsync(request.Id, CancellationToken.None)).ReturnsAsync(league);
        _leagueRepository.Setup(x => x.UpdateAsync(It.IsAny<League>(), CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _leagueRepository.Verify(x => x.GetByIdAsync(request.Id, CancellationToken.None), Times.Once);
        _leagueRepository.Verify(x => x.UpdateAsync(It.IsAny<League>(), CancellationToken.None), Times.Once);
    }
    
}