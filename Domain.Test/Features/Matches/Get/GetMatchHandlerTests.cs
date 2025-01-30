using Domain.Features.Matches.Get;
using Domain.Repositories;
using Moq;
using Match = Domain.Models.Match;

namespace Domain.Test.Features.Matches.Get;

public class GetMatchHandlerTests
{
    private GetMatchHandler _handler;
    private Mock<IMatchRepository> _matchRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _handler = new GetMatchHandler(_matchRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Match()
    {
        var match = new Match
        {
            Id = Guid.NewGuid(),
            HomeTeamId = Guid.NewGuid(),
            AwayTeamId = Guid.NewGuid(),
            CreatedAt = DateTime.Now
        };
        _matchRepository.Setup(x => x.GetByIdAsync(match.Id, CancellationToken.None)).ReturnsAsync(match);
        var query = new GetMatchQuery { Id = match.Id };
        
        var result = await _handler.Handle(query, CancellationToken.None);
        
        _matchRepository.Verify(x => x.GetByIdAsync(match.Id, CancellationToken.None), Times.Once);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Match.Id, Is.EqualTo(match.Id));
        Assert.That(result.Match.HomeTeamId, Is.EqualTo(match.HomeTeamId));
        Assert.That(result.Match.AwayTeamId, Is.EqualTo(match.AwayTeamId));
        Assert.That(result.Match.CreatedAt, Is.EqualTo(match.CreatedAt)); 
    }
}