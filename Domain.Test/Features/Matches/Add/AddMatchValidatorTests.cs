using Domain.Features.Matches.Add;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Matches.Add;
[TestFixture]
public class AddMatchValidatorTests
{
    private AddMatchValidator _validator;
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _validator = new AddMatchValidator(_teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_HomeTeamId_Is_Empty()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.Empty, AwayTeamId = Guid.NewGuid() };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.HomeTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_AwayTeamId_Is_Empty()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.AwayTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_HomeTeam_Not_Exists()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.HomeTeamId);
    }
    
    [Test]
    public async Task Should_Return_Error_When_AwayTeam_Not_Exists()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.AwayTeamId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.AwayTeamId);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_HomeTeam_And_AwayTeam_Exists()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(request.HomeTeamId, CancellationToken.None)).ReturnsAsync(true);
        _teamRepository.Setup(x => x.ExistsAsync(request.AwayTeamId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.HomeTeamId);
        result.ShouldNotHaveValidationErrorFor(x => x.AwayTeamId);
    }
}