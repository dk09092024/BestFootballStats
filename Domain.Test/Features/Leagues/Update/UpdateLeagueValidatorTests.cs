using Domain.Features.Leagues.Update;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Leagues.Update;
[TestFixture]
public class UpdateLeagueValidatorTests
{
    private UpdateLeagueValidator _validator;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _validator = new UpdateLeagueValidator(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var request = new UpdateLeagueRequest { Id = Guid.Empty, Name = "Test" };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Name_Is_Empty()
    {
        var request = new UpdateLeagueRequest { Id = Guid.NewGuid(), Name = string.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task Should_Return_Error_When_League_Not_Exists()
    {
        var request = new UpdateLeagueRequest { Id = Guid.NewGuid(), Name = "Test" };
        _leagueRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_League_Exists()
    {
        var request = new UpdateLeagueRequest { Id = Guid.NewGuid(), Name = "Test" };
        _leagueRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}