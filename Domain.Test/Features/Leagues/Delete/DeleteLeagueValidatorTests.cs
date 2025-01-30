using Domain.Features.Leagues.Delete;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Leagues.Delete;
[TestFixture]
public class DeleteLeagueValidatorTests
{
    private DeleteLeagueValidator _validator;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _validator = new DeleteLeagueValidator(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var request = new DeleteLeagueRequest { Id = Guid.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_League_Not_Exists()
    {
        var request = new DeleteLeagueRequest { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_League_Exists()
    {
        var request = new DeleteLeagueRequest { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}