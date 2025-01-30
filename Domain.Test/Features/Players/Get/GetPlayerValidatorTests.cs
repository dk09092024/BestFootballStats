using Domain.Features.Players.Get;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Players.Get;
[TestFixture]
public class GetPlayerValidatorTests
{
    private GetPlayerValidator _validator;
    private Mock<IPlayerRepository> _playerRepository;

    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _validator = new GetPlayerValidator(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var playerId = Guid.Empty;
        var result = await _validator.TestValidateAsync(new GetPlayerQuery(playerId));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Test]
    public async Task Should_Have_Error_When_Player_Does_Not_Exist()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(new GetPlayerQuery(playerId));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Have_Error_When_Player_Exists()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(new GetPlayerQuery(playerId));
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
    
}