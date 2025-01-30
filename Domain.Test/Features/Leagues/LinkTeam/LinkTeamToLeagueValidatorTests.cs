using Domain.Features.Leagues.LinkTeam;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Leagues.LinkTeam;
[TestFixture]
public class LinkTeamToLeagueValidatorTests
{
    private LinkTeamToLeagueValidator _validator;
    private Mock<ILeagueRepository> _leagueRepository;
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _validator = new LinkTeamToLeagueValidator(_leagueRepository.Object, _teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_League_Id_Is_Empty()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.Empty, TeamId = Guid.NewGuid() };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.LeagueId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Team_Id_Is_Empty()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.NewGuid(), TeamId = Guid.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.TeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_League_Not_Exists()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.NewGuid(), TeamId = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.LeagueId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.LeagueId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Team_Not_Exists()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.NewGuid(), TeamId = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.LeagueId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.TeamId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.TeamId);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_League_And_Team_Exists()
    {
        var request = new LinkTeamToLeagueRequest { LeagueId = Guid.NewGuid(), TeamId = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.LeagueId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.TeamId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.LeagueId);
        result.ShouldNotHaveValidationErrorFor(x => x.TeamId);
    }
}