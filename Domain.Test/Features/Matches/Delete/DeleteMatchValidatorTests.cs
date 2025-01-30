using Domain.Features.Matches.Delete;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Matches.Delete;
[TestFixture]
public class DeleteMatchValidatorTests
{
    private DeleteMatchValidator _validator;
    private Mock<IMatchRepository> _matchRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _validator = new DeleteMatchValidator(_matchRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var request = new DeleteMatchRequest { Id = Guid.Empty };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Match_Not_Exists()
    {
        var request = new DeleteMatchRequest { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_Match_Exists()
    {
        var request = new DeleteMatchRequest { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.ExistsAsync(request.Id, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
    
}