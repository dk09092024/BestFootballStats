using Domain.Models.Enum;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Players.Add;

public class AddPlayerValidator : AbstractValidator<AddPlayerRequest>
{
    public AddPlayerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Name must be between 3 and 100 characters");
        RuleFor(x => x.Position)
            .NotEmpty()
            .WithMessage("Position is required")
            .IsInEnum()
            .WithMessage("Position is invalid");
    }
}