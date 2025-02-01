using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Test.Repositories;
[TestFixture]
public class TeamRepositoryTests
{
    private TeamRepository _teamRepository;
    private Mock<ILeagueRepository> _leagueRepository;
    private ApplicationContext _context;
 
    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared")
            .Options;
        _context = new ApplicationContext(options);
        await _context.Database.EnsureCreatedAsync();
        _leagueRepository = new Mock<ILeagueRepository>();
        _teamRepository = new TeamRepository(_context,_leagueRepository.Object);
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
}