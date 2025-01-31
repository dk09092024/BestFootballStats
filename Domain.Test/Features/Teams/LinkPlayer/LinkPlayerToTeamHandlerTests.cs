using Domain.Features.Teams.LinkPlayer;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Teams.LinkPlayer;
[TestFixture]
public class LinkPlayerToTeamHandlerTests
{
    private LinkPlayerToTeamHandler _handler;
    private Mock<IPlayerRepository> _playerRepository;
    private Mock<ITeamRepository> _teamRepository;

    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new LinkPlayerToTeamHandler(_playerRepository.Object);
    }

    [Test]
    public async Task Should_Link_Player_To_Team()
    {
        var request = new LinkPlayerToTeamRequest { PlayerId = Guid.NewGuid(), TeamId = Guid.NewGuid() };

        _playerRepository.Setup(x => x.LinkToTeam(request.PlayerId, request.TeamId, CancellationToken.None));

        await _handler.Handle(request, CancellationToken.None);

        _playerRepository.Verify(x => x.LinkToTeam(request.PlayerId, request.TeamId, CancellationToken.None), Times.Once);
    }
}