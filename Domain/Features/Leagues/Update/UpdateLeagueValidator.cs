using FluentValidation;

namespace Domain.Features.Leagues.Update;

public class UpdateLeagueValidator : AbstractValidator<UpdateLeagueRequest>
{
    public UpdateLeagueValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Leagues name is required.")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Leagues name must be between 3 and 100 characters.");
    }
}