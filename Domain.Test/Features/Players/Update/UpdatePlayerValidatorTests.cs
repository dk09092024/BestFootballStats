using Domain.Features.Players.Update;
using Domain.Models.Enum;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Players.Update;
[TestFixture]
public class UpdatePlayerValidatorTests
{
    private UpdatePlayerValidator _validator;
    private Mock<IPlayerRepository> _playerRepository;
    
    [SetUp]
    public void SetUp()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _validator = new UpdatePlayerValidator(_playerRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Id_Is_Empty()
    {
        var playerId = Guid.Empty;
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, "name", (Position)1));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Name_Is_Empty()
    {
        var playerId = Guid.NewGuid();
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, "", (Position)1));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Name_Is_Spaces()
    {
        var playerId = Guid.NewGuid();
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, " ", (Position)1));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task Should_Return_Error_When_Position_Is_Not_In_Enum()
    {
        var playerId = Guid.NewGuid();
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, "name", (Position)(0)));
        result.ShouldHaveValidationErrorFor(x => x.Position);
    }
    
    [Test]
    public async Task Should_Have_Error_When_Player_Does_Not_Exist()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(false);
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, "name", (Position)1));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Have_Error_When_Player_Exists()
    {
        var playerId = Guid.NewGuid();
        _playerRepository.Setup(x => x.ExistsAsync(playerId, CancellationToken.None)).ReturnsAsync(true);
        var result = await _validator.TestValidateAsync(new UpdatePlayerRequest(playerId, "name", (Position)1));
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }   
    
}