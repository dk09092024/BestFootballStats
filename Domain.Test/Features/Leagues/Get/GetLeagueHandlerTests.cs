using Domain.Features.Leagues.Get;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Leagues.Get;
[TestFixture]
public class GetLeagueHandlerTests
{
    private GetLeagueHandler _handler;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _handler = new GetLeagueHandler(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Return_League()
    {
        var league = new League
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            CreatedAt = DateTime.Now
        };
        _leagueRepository.Setup(x => x.GetByIdAsync(league.Id, CancellationToken.None)).ReturnsAsync(league);
        var query = new GetLeagueQuery { Id = league.Id };
        
        var result = await _handler.Handle(query, CancellationToken.None);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.League.Id, Is.EqualTo(league.Id));
        Assert.That(result.League.Name, Is.EqualTo(league.Name));
        Assert.That(result.League.CreatedAt, Is.EqualTo(league.CreatedAt)); 
    }
}