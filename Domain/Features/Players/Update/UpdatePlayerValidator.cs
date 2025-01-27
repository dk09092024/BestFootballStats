using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Players.Update;

public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerRequest>
{
    public UpdatePlayerValidator(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .MustAsync(async (x, cancellationToken) => await playerRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Player does not exist");
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