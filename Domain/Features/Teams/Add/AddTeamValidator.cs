using FluentValidation;

namespace Domain.Features.Teams.Add;

public class AddTeamValidator : AbstractValidator<AddTeamRequest>
{
    public AddTeamValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Name must be between 3 and 100 characters");
    }
}