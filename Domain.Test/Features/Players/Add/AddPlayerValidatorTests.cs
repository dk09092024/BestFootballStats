using Domain.Features.Players.Add;
using Domain.Models.Enum;
using FluentValidation.TestHelper;

namespace Domain.Test.Features.Players.Add;
[TestFixture]
public class AddPlayerValidatorTests
{
    private AddPlayerValidator _validator;
    
    [SetUp]
    public void SetUp()
    {
        _validator = new AddPlayerValidator();
    }
    
    [Test]
    public async Task Should_Return_Error_When_Name_Is_Empty()
    {
        var request = new AddPlayerRequest { Name = String.Empty , Position = Position.Goalkeeper};
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public async Task Should_Return_Error_When_Name_Is_Spaces()
    {
        var request = new AddPlayerRequest { Name = " " , Position = Position.Goalkeeper};
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task? Should_Return_Error_When_Position_Is_Not_In_Enum()
    {
        var request = new AddPlayerRequest { Name = "Test", Position = (Position)(0) };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(x => x.Position);
    }
    
    [Test]
    public async Task Should_Not_Return_Error_When_Name_And_Position_Exists()
    {
        var request = new AddPlayerRequest { Name = "Test", Position = (Position)1 };
        var result = await _validator.TestValidateAsync(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.Position);
    }

}