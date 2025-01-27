using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.Get;

public class GetMatchValidator : AbstractValidator<GetMatchQuery>
{
    public GetMatchValidator(IMatchRepository matchRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Match ID is required.")
            .MustAsync(async (x, cancellationToken) => await matchRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Match does not exist.");
    }
}