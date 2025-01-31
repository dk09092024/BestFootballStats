using Domain.Features.Teams.Delete;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Teams.Delete;
[TestFixture]
public class DeleteTeamHandlerTests
{
    private DeleteTeamHandler _handler; 
    private Mock<ITeamRepository> _teamRepository;
    
    [SetUp]
    public void SetUp()
    {
        _teamRepository = new Mock<ITeamRepository>();
        _handler = new DeleteTeamHandler(_teamRepository.Object);
    }
    
    [Test]
    public async Task Should_Delete_Team()
    {
        var request = new DeleteTeamRequest() { Id = Guid.NewGuid() };
        _teamRepository.Setup(x => x.DeleteAsync(request.Id, CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _teamRepository.Verify(x => x.DeleteAsync(request.Id, CancellationToken.None), Times.Once);
    }
}