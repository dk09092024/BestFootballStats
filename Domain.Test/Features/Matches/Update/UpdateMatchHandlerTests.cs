using Domain.Features.Matches.Update;
using Domain.Repositories;
using Moq;
using Match = Domain.Models.Match;

namespace Domain.Test.Features.Matches.Update;

public class UpdateMatchHandlerTests
{
    private UpdateMatchHandler _handler;
    private Mock<IMatchRepository> _matchRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _handler = new UpdateMatchHandler(_matchRepository.Object);
    }
    
    [Test]
    public async Task Should_Update_Match()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid()};
        var match = new Match
        {
            Id = request.Id,
            CreatedAt = DateTime.Now,
            HomeTeamId = request.HomeTeamId,
            AwayTeamId = request.AwayTeamId
        };
        _matchRepository.Setup(x => x.GetByIdAsync(request.Id, CancellationToken.None)).ReturnsAsync(match);
        _matchRepository.Setup(x => x.UpdateAsync(It.IsAny<Match>(), CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _matchRepository.Verify(x => x.GetByIdAsync(request.Id, CancellationToken.None), Times.Once);
        _matchRepository.Verify(x => x.UpdateAsync(It.IsAny<Match>(), CancellationToken.None), Times.Once);
    }
}