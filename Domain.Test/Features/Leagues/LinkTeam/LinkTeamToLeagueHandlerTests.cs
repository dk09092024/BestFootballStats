using Domain.Features.Leagues.LinkTeam;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Leagues.LinkTeam;
[TestFixture]
public class LinkTeamToLeagueHandlerTests
{
    private LinkTeamToLeagueHandler _handler;
    private Mock<ILeagueRepository> _leagueRepository;
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new LinkTeamToLeagueHandler(_leagueRepository.Object, _teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Link_Team_To_League()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.NewGuid(), TeamId = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.LeagueId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.TeamId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.LinkToLeague(request.TeamId, request.LeagueId, CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _teamRepository.Verify(x => x.LinkToLeague(request.TeamId, request.LeagueId, CancellationToken.None), Times.Once);
    }
}