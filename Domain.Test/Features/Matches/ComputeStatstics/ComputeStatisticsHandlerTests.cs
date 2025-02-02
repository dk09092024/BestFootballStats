using Domain.Features.Matches.ComputeStatistics;
using Domain.Repositories;
using Moq;
using Match = Domain.Models.Match;

namespace Domain.Test.Features.Matches.ComputeStatstics;
[TestFixture]
public class ComputeStatisticsHandlerTests
{
    private Mock<IMatchRepository> _matchRepository;
    private ComputeStatisticsHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>(); 
        _handler = new ComputeStatisticsHandler(_matchRepository.Object);
    }
    
    [Test]
    public async Task Handle_ShouldReturnMatchStatistics()
    {
        var match = new Match
        {
            HomeTeamId = Guid.NewGuid(),
            AwayTeamId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            CreatedAt = default
        };
        _matchRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(match);
        
        await _handler.Handle(new ComputeStatisticsRequest { MatchId = match.Id }, CancellationToken.None);
        
        _matchRepository.Verify(x => x.UpdateAsync(It.IsAny<Match>(), It.IsAny<CancellationToken>()), Times.Once);
        _matchRepository.Verify(x => x.UpdateAsync(It.Is<Match>(x => x.TotalPasses >=100 && x.TotalPasses <=1000), It.IsAny<CancellationToken>()), Times.Once);
    }
}