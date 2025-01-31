using Domain.Features.Teams.Get;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Teams.Get;
[TestFixture]
public class GetTeamHandlerTests
{
    private GetTeamHandler _handler; 
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new GetTeamHandler(_teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Get_Team()
    {
        var request = new GetTeamQuery() { Id = Guid.NewGuid() };
        var team = new Team
        {
            Id = request.Id,
            Name = "Test",
            CreatedAt = default
        };
        _teamRepository.Setup(x => x.GetByIdAsync(request.Id, CancellationToken.None)).ReturnsAsync(team);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        _teamRepository.Verify(x => x.GetByIdAsync(request.Id, CancellationToken.None), Times.Once);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Team, Is.Not.Null);
        Assert.That(result.Team.Id, Is.EqualTo(request.Id));
        Assert.That(result.Team.Name, Is.EqualTo(team.Name));
        Assert.That(result.Team.CreatedAt, Is.EqualTo(team.CreatedAt));
    }
}