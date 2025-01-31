using Domain.Features.Teams.Update;
using Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Domain.Test.Features.Teams.Update;

[TestFixture]
public class UpdateTeamValidatorTest
{
    private UpdateTeamValidator _validator;
    private Mock<ITeamRepository> _teamRepository;

    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _validator = new UpdateTeamValidator(_teamRepository.Object);
    }

    [Test]
    public async Task Should_Have_Error_When_Id_Is_Empty()
    {
        var model = new UpdateTeamRequest { Id = Guid.Empty, Name = "Team" };

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Test]
    public async Task Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new UpdateTeamRequest { Id = Guid.NewGuid(), Name = string.Empty };

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    public async Task Should_Have_Error_When_Id_Does_Not_Exist()
    {
        var model = new UpdateTeamRequest { Id = Guid.NewGuid(), Name = "Team" };
        _teamRepository.Setup(x => x.ExistsAsync(model.Id, CancellationToken.None)).ReturnsAsync(false);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Id_Exists_And_Name_Is_Valid()
    {
        var model = new UpdateTeamRequest { Id = Guid.NewGuid(), Name = "Team" };
        _teamRepository.Setup(x => x.ExistsAsync(model.Id, CancellationToken.None)).ReturnsAsync(true);

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);

    }
}