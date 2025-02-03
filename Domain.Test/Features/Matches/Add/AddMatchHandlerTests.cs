using System.Linq.Expressions;
using Domain.Features.Matches.Add;
using Domain.Repositories;
using MediatR;
using Moq;
using Match = Domain.Models.Match;
using Hangfire;
using Hangfire.InMemory;

namespace Domain.Test.Features.Matches.Add;
[TestFixture]
public class AddMatchHandlerTests
{
    private AddMatchHandler _handler;
    private Mock<IMatchRepository> _matchRepository;
    private Mock<IMediator> _mediator;
    

    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _mediator = new Mock<IMediator>();
        _handler = new AddMatchHandler(_mediator.Object, _matchRepository.Object);
        JobStorage.Current = new InMemoryStorage();
    }
    
    [Test]
    public async Task Should_Add_Match()
    {
        var request = new AddMatchRequest { HomeTeamId = Guid.NewGuid(), AwayTeamId = Guid.NewGuid() };
        var matchId = Guid.NewGuid();
        _matchRepository.Setup(x => x.AddAsync(It.IsAny<Match>(), CancellationToken.None)).ReturnsAsync(matchId);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        _matchRepository.Verify(x => x.AddAsync(It.IsAny<Match>(), CancellationToken.None), Times.Once);
        Assert.That(result.Id, Is.EqualTo(matchId));
    }

}