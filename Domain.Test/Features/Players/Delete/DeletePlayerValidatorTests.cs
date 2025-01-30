using Domain.Features.Players.Delete;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Players.Delete;
[TestFixture]
public class DeletePlayerValidatorTests
{
    private DeletePlayerValidator _validator;
    private Mock<IPlayerRepository> _playerRepository;

    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _validator = new DeletePlayerValidator(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var request = new DeletePlayerRequest(Guid.Empty);
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Test]
    public async Task Should_Have_Error_When_Player_Does_Not_Exist()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(new DeletePlayerRequest(playerId));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Have_Error_When_Player_Exists()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(new DeletePlayerRequest(playerId));
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}