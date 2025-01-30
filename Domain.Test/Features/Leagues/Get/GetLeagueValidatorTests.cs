using Domain.Features.Leagues.Get;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Leagues.Get;
[TestFixture]
public class GetLeagueValidatorTests
{
    private GetLeagueValidator _validator;
    private Mock<ILeagueRepository> _leagueRepository;

    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _validator = new GetLeagueValidator(_leagueRepository.Object);
    }

    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var query = new GetLeagueQuery { Id = Guid.Empty };

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Not_Empty()
    {
        var query = new GetLeagueQuery { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_League_Does_Not_Exist()
    {
        var query = new GetLeagueQuery { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_League_Exists()
    {
        var query = new GetLeagueQuery { Id = Guid.NewGuid() };
        _leagueRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(true);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}