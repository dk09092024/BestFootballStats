using Domain.Features.Teams.Update;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Teams.Update;
[TestFixture]
public class UpdateTeamHandlerTests
{
    private UpdateTeamHandler _handler;
    private Mock<ITeamRepository> _teamRepository;

    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new UpdateTeamHandler(_teamRepository.Object);
    }

    [Test]
    public async Task Should_Update_Team()
    {
        var request = new UpdateTeamRequest() { Id = Guid.NewGuid(), Name = "Test" };
        var team = new Team
        {
            Id = request.Id,
            Name = request.Name,
            CreatedAt = default
        };
        _teamRepository.Setup(x => x.GetByIdAsync(request.Id, CancellationToken.None)).ReturnsAsync(team);
        _teamRepository.Setup(x => x.UpdateAsync(team, CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _teamRepository.Verify(x => x.UpdateAsync(team, CancellationToken.None), Times.Once);
    }
    
}