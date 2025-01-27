using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Leagues.Add;

public class AddLeagueHandler : IRequestHandler<AddLeagueRequest, AddLeagueResult>
{
    private readonly ILeagueRepository _leagueRepository;

    public AddLeagueHandler(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }

    public async Task<AddLeagueResult> Handle(AddLeagueRequest request, CancellationToken cancellationToken)
    {
        var league = new League()
        {
            Id = default,
            CreatedAt = default,
            Name = request.Name
        };
        await _leagueRepository.AddAsync(league,cancellationToken);
        return new AddLeagueResult(league.Id);
    }
}