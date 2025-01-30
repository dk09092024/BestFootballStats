using Domain.Features.Leagues.Add;
using FluentValidation.TestHelper;

namespace Domain.Test.Features.Leagues.Add;
[TestFixture]
public class AddLeagueValidatorTests
{
    private AddLeagueValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AddLeagueValidator();
    }

    [Test]
    public async Task Should_Have_Error_When_Name_Is_Null()
    {
        var command = new AddLeagueRequest() { Name = null };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public async Task Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new AddLeagueRequest { Name = string.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public async Task Should_Have_Error_When_Name_Is_WhiteSpace()
    {
        var command = new AddLeagueRequest { Name = " " };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var command = new AddLeagueRequest { Name = "League" };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    
}