using Domain.Features.Teams.Add;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Teams.Add;
[TestFixture]
public class AddTeamHandlerTests
{
    private AddTeamHandler _handler;
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new AddTeamHandler(_teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Add_Team()
    {
        var request = new AddTeamRequest() { Name = "Test" };
        var teamId = Guid.NewGuid();
        _teamRepository.Setup(x => x.AddAsync(It.IsAny<Team>(), CancellationToken.None)).ReturnsAsync(teamId);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        
        _teamRepository.Verify(x => x.AddAsync(It.IsAny<Team>(), CancellationToken.None), Times.Once);
        Assert.That(result.Id, Is.EqualTo(teamId));
    }
}