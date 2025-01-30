using Domain.Features.Matches.Delete;
using Domain.Repositories;
using Moq;

namespace Domain.Test.Features.Matches.Delete;
[TestFixture]
public class DeleteMatchHandlerTests
{
    private DeleteMatchHandler _handler;
    private Mock<IMatchRepository> _matchRepository;
    
    [SetUp]
    public void SetUp()
    {
        _matchRepository = new Mock<IMatchRepository>();
        _handler = new DeleteMatchHandler(_matchRepository.Object);
    }
    
    [Test]
    public async Task Should_Delete_Match()
    {
        var request = new DeleteMatchRequest { Id = Guid.NewGuid() };
        _matchRepository.Setup(x => x.DeleteAsync(request.Id, CancellationToken.None));
        
        await _handler.Handle(request, CancellationToken.None);
        
        _matchRepository.Verify(x => x.DeleteAsync(request.Id, CancellationToken.None), Times.Once);
    }
}