using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Infrastructure.Test.Repositories;
[TestFixture]
public class LeagueRepositoryTests
{
    private LeagueRepository _leagueRepository;
    private ApplicationContext _context;
    
    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared")
            .Options;
        _context = new ApplicationContext(options);
        await _context.Database.EnsureCreatedAsync();
        _leagueRepository = new LeagueRepository(_context);
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
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        await _leagueRepository.DeleteAsync(id, CancellationToken.None);
        Assert.ThrowsAsync<EntityDoesNotExistExeption<LeagueRepository, League>>(async () =>
        {
            await _leagueRepository.MustExistAsync(id, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task MustExistAsync_ShouldNotThrowException()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        await _leagueRepository.MustExistAsync(id, CancellationToken.None);
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnTrue()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        var result = await _leagueRepository.ExistsAsync(id, CancellationToken.None);
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnFalse()
    {
        var result = await _leagueRepository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task DeleteAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<LeagueRepository, League>>(async () =>
        {
            await _leagueRepository.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task UpdateAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<LeagueRepository, League>>(async () =>
        {
            await _leagueRepository.UpdateAsync(new League
            {
                Name = "Test",
                Id = Guid.NewGuid(),
                CreatedAt = default
            }, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<LeagueRepository, League>>(async () =>
        {
            await _leagueRepository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task AddAsync_ShouldAddLeague()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        var result = await _context.Leagues.FirstOrDefaultAsync(x => x.Id == id);
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldReturnLeague()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        var result = await _leagueRepository.GetByIdAsync(id, CancellationToken.None);
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public async Task UpdateAsync_ShouldUpdateLeague()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        var result = await _leagueRepository.GetByIdAsync(id, CancellationToken.None);
        result.Name = "Updated";
        await _leagueRepository.UpdateAsync(result, CancellationToken.None);
        var updated = await _leagueRepository.GetByIdAsync(id, CancellationToken.None);
        Assert.That(updated.Name, Is.EqualTo("Updated"));
    }   
    
    [Test]
    public async Task DeleteAsync_ShouldDeleteLeague()
    {
        var league = new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        };
        var id = await _leagueRepository.AddAsync(league, CancellationToken.None);
        await _leagueRepository.DeleteAsync(id, CancellationToken.None);
        var result = await _context.Leagues.FirstOrDefaultAsync(x => x.Id == id);
        Assert.That(result, Is.Null);
    }
}