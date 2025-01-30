using Domain.Features.Matches.Update;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Matches.Update;
[TestFixture]
public class UpdateMatchValidatorTests
{
    private UpdateMatchValidator _validator;
    private Mock<IMatchRepository> _matchRepository;
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _validator = new UpdateMatchValidator(_matchRepository.Object, _teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_MatchId_Is_Empty()
    {
        var request = new UpdateMatchRequest { Id = Guid.Empty, HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_HomeTeamId_Is_Empty()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.Empty, AwayTeamId = Guid.NewGuid() };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.HomeTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_AwayTeamId_Is_Empty()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.AwayTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Match_Not_Exists()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_HomeTeam_Not_Exists()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.HomeTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_AwayTeam_Not_Exists()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.AwayTeamId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.AwayTeamId);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_Match_And_Teams_Exists()
    {
        var request = new UpdateMatchRequest { Id = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.AwayTeamId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
        result.ShouldNotHaveValidationErrorFor(x => x.HomeTeamId);
        result.ShouldNotHaveValidationErrorFor(x => x.AwayTeamId);
    }
    
    
}