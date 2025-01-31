using Domain.Features.Teams.Delete;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Teams.Delete;
[TestFixture]
public class DeleteTeamValidatorTests
{
    private DeleteTeamValidator _validator;
    private Mock<ITeamRepository> _teamRepository; 

    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _validator = new DeleteTeamValidator(_teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        var model = new DeleteTeamRequest { Id = Guid.Empty };

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Have_Error_When_Team_Does_Not_Exist()
    {
        var model = new DeleteTeamRequest { Id = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(model.Id, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    public async Task Should_Not_Have_Error_When_Team_Exists()
    {
        var model = new DeleteTeamRequest { Id = Guid.NewGuid() };
        _teamRepository.Setup(x => x.ExistsAsync(model.Id, CancellationToken.None)).ReturnsAsync(true);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}