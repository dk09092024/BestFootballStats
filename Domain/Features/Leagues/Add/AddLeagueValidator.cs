using FluentValidation;

namespace Domain.Features.Leagues.Add;

public class AddLeagueValidator : AbstractValidator<AddLeagueRequest>
{
    public AddLeagueValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Leagues name is required.")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Leagues name must be between 3 and 100 characters.");
    }
}