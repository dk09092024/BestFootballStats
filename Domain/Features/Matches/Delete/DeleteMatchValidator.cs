using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.Delete;

public class DeleteMatchValidator : AbstractValidator<DeleteMatchRequest>
{
    public DeleteMatchValidator(IMatchRepository matchRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Match ID is required.")
            .MustAsync(async (x, cancellationToken) => await matchRepository.ExistsAsync(x, cancellationToken))
            .WithMessage("Match does not exist.");
    }
    
}