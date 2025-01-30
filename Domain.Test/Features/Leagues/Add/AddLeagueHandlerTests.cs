using Domain.Features.Leagues.Add;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Leagues.Add;
[TestFixture]
public class AddLeagueHandlerTests
{
    private AddLeagueHandler _handler;
    private Mock<ILeagueRepository> _leagueRepository;
    
    [SetUp]
    public void SetUp()
    {
        _leagueRepository = new Mock<ILeagueRepository>();
        _handler = new AddLeagueHandler(_leagueRepository.Object);
    }
    
    [Test]
    public async Task Should_Add_League()
    {
        var command = new AddLeagueRequest { Name = "League" };
        var id = Guid.NewGuid();
        _leagueRepository.Setup(x => x.AddAsync(It.IsAny<League>(), CancellationToken.None)).ReturnsAsync(id);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.That(result.Id, Is.EqualTo(id));
        _leagueRepository.Verify(x=>x.AddAsync(It.IsAny<League>(), CancellationToken.None), Times.Once);
    }
    
    
}