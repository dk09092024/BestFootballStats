using Domain.Features.Teams.Add;
using FluentValidation.TestHelper;

namespace Domain.Test.Features.Teams.Add;
[TestFixture]
public class AddTeamValidatorTests
{
    private AddTeamValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AddTeamValidator();
    }

    [Test]
    public void Should_have_error_when_Name_is_null()
    {
        var model = new AddTeamRequest { Name = null };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public void Should_have_error_when_Name_is_empty()
    {
        var model = new AddTeamRequest() { Name = string.Empty };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
        
    [Test]
    public void Should_not_have_error_when_Name_is_valid()
    {
        var model = new AddTeamRequest() { Name = "Test" };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    


    
    
}