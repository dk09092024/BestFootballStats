using Domain.Features.Matches.Get;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Matches.Get;
[TestFixture]
public class GetMatchValidatorTests
{
    private GetMatchValidator _validator;
    private Mock<IMatchRepository> _matchRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _validator = new GetMatchValidator(_matchRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var query = new GetMatchQuery { Id = Guid.Empty };
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Not_Empty()
    {
        var query = new GetMatchQuery { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Match_Does_Not_Exist()
    {
        var query = new GetMatchQuery { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_Match_Exists()
    {
        var query = new GetMatchQuery { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(query.Id, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(query);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}