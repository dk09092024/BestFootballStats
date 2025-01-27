using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Players.Get;

public class GetPlayerValidator : AbstractValidator<GetPlayerQuery>
{
    public GetPlayerValidator(IPlayerRepository playerRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .MustAsync(async (x, cancellationToken) => await playerRepository.ExistsAsync(x))
            .WithMessage("Player does not exist.");
    }
}