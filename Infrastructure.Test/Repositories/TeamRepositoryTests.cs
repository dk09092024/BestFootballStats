using Domain.Models;
using Domain.Models.Enum;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Exceptions;
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
   [Test]
       public async Task MustExistAsync_ShouldThrowException()
       {
           Assert.ThrowsAsync<EntityDoesNotExistExeption<TeamRepository, Team>>(async () =>
           {
               await _teamRepository.MustExistAsync(Guid.NewGuid(), CancellationToken.None);
           });
       }
   
       [Test]
       public async Task MustExistAsync_ShouldNotThrowException()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           await _context.Teams.AddAsync(team);
           await _context.SaveChangesAsync();
           await _teamRepository.MustExistAsync(team.Id, CancellationToken.None);
       }
   
       [Test]
       public async Task AddAsync_ShouldReturnId()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           var id = await _teamRepository.AddAsync(team, CancellationToken.None);
           Assert.That(id, Is.Not.EqualTo(default(Guid)));
       }
   
       [Test]
       public async Task UpdateAsync_ShouldUpdate()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           var id = await _teamRepository.AddAsync(team, CancellationToken.None);
           team.Name = "Test2";
           await _teamRepository.UpdateAsync(team, CancellationToken.None);
           var updatedTeam = await _context.Teams.FindAsync(id);
           Assert.That(updatedTeam.Name, Is.EqualTo("Test2"));
       }
   
       [Test]
       public async Task UpdateAsync_ShouldThrowException()
       {
           Assert.ThrowsAsync<EntityDoesNotExistExeption<TeamRepository, Team>>(async () =>
           {
               await _teamRepository.UpdateAsync(new Team
               {
                   Name = "Test",
                   Id = Guid.NewGuid(),
                   CreatedAt = default,
               }, CancellationToken.None);
           });
       }
   
       [Test]
       public async Task DeleteAsync_ShouldDelete()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           var id = await _teamRepository.AddAsync(team, CancellationToken.None);
           await _teamRepository.DeleteAsync(id, CancellationToken.None);
           var deletedTeam = await _context.Teams.FindAsync(id);
           Assert.That(deletedTeam, Is.Null);
       }
   
       [Test]
       public async Task DeleteAsync_ShouldThrowException()
       {
           Assert.ThrowsAsync<EntityDoesNotExistExeption<TeamRepository, Team>>(async () =>
           {
               await _teamRepository.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
           });
       }
   
       [Test]
       public async Task ExistsAsync_ShouldReturnTrue()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           await _context.Teams.AddAsync(team);
           await _context.SaveChangesAsync();
           var result = await _teamRepository.ExistsAsync(team.Id, CancellationToken.None);
           Assert.That(result, Is.True);
       }
   
       [Test]
       public async Task ExistsAsync_ShouldReturnFalse()
       {
           var result = await _teamRepository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);
           Assert.That(result, Is.False);
       }
   
       [Test]
       public async Task GetByIdAsync_ShouldReturnTeam()
       {
           var team = new Team
           {
               Name = "Test",
               Id = default,
               CreatedAt = default,
           };
           await _context.Teams.AddAsync(team);
           await _context.SaveChangesAsync();
           var result = await _teamRepository.GetByIdAsync(team.Id, CancellationToken.None);
           Assert.That(result, Is.Not.Null);
       }
   
       [Test]
       public async Task GetByIdAsync_ShouldThrowException()
       {
           Assert.ThrowsAsync<EntityDoesNotExistExeption<TeamRepository, Team>>(async () =>
           {
               await _teamRepository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
           });
       }
}