using Domain.Features.Teams.LinkPlayer;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Teams.LinkPlayer;
[TestFixture]
public class LinkPlayerToTeamValidatorTests
{
    private LinkPlayerToTeamValidator _validator;
    private Mock<ITeamRepository> _teamRepository;
    private Mock<IPlayerRepository> _playerRepository;

    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _playerRepository = new Mock<IPlayerRepository>();
        _validator = new LinkPlayerToTeamValidator(_playerRepository.Object, _teamRepository.Object);
    }

    [Test]
    public async Task Should_Have_Error_When_TeamId_Is_Empty()
    {
        var model = new LinkPlayerToTeamRequest { TeamId = Guid.Empty, PlayerId = Guid.NewGuid() };

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.TeamId);
    }

    [Test]
    public async Task Should_Have_Error_When_PlayerId_Is_Empty()
    {
        var model = new LinkPlayerToTeamRequest { TeamId = Guid.NewGuid(), PlayerId = Guid.Empty };

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.PlayerId);
    }

    [Test]
    public async Task Should_Have_Error_When_TeamId_Does_Not_Exist()
    {
        var model = new LinkPlayerToTeamRequest { TeamId = Guid.NewGuid(), PlayerId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(model.TeamId, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.TeamId);
    }

    [Test]
    public async Task Should_Have_Error_When_PlayerId_Does_Not_Exist()
    {
        var model = new LinkPlayerToTeamRequest { TeamId = Guid.NewGuid(), PlayerId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(model.TeamId, CancellationToken.None)).ReturnsAsync(true);
        _playerRepository.Setup(x => x.ExistsAsync(model.PlayerId, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.PlayerId);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_TeamId_And_PlayerId_Exist()
    {
        var model = new LinkPlayerToTeamRequest { TeamId = Guid.NewGuid(), PlayerId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(model.TeamId, CancellationToken.None)).ReturnsAsync(true);
        _playerRepository.Setup(x => x.ExistsAsync(model.PlayerId, CancellationToken.None)).ReturnsAsync(true);
        
        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveAnyValidationErrors();
    }
        
        
}