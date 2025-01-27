using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Players.Delete;

public class DeletePlayerValidator : AbstractValidator<DeletePlayerRequest>
{
    public DeletePlayerValidator(IPlayerRepository playerRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .MustAsync(async (x, cancellationToken) => await playerRepository.ExistsAsync(x))
            .WithMessage("Player does not exist.");
    }
}