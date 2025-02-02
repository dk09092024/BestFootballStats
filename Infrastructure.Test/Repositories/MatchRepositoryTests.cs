using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Match = Domain.Models.Match;

namespace Infrastructure.Test.Repositories;
[TestFixture]
public class MatchRepositoryTests
{
    private MatchRepository _matchRepository;
    private ApplicationContext _context;
 
    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared")
            .Options;
        _context = new ApplicationContext(options);
        await _context.Database.EnsureCreatedAsync();
        _matchRepository = new MatchRepository(_context);
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
        Assert.ThrowsAsync<EntityDoesNotExistExeption<MatchRepository, Match>>(async () =>
        {
            await _matchRepository.MustExistAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task MustExistAsync_ShouldNotThrowException()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        await _matchRepository.MustExistAsync(id, CancellationToken.None);
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnTrue()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        var exists = await _matchRepository.ExistsAsync(id, CancellationToken.None);
        Assert.That(exists, Is.True);
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnFalse()
    {
        var exists = await _matchRepository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);
        Assert.That(exists, Is.False);
    }
    
    [Test]
    public async Task DeleteAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<MatchRepository, Match>>(async () =>
        {
            await _matchRepository.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task DeleteAsync_ShouldNotThrowException()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        await _matchRepository.DeleteAsync(id, CancellationToken.None);
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldReturnMatch()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        var result = await _matchRepository.GetByIdAsync(id, CancellationToken.None);
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<MatchRepository, Match>>(async () =>
        {
            await _matchRepository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task AddAsync_ShouldAddMatch()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        var result = await _context.Matches.FirstOrDefaultAsync(x => x.Id == id);
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public async Task UpdateAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<MatchRepository, Match>>(async () =>
        {
            await _matchRepository.UpdateAsync(new Match
            {
                Id = Guid.NewGuid(),
                CreatedAt = default,
                HomeTeamId = Guid.NewGuid(),
                AwayTeamId = Guid.NewGuid(),
                HomeTeam = new Team
                {
                    Name = "TestHome",
                    Id = Guid.NewGuid(),
                    CreatedAt = default
                },
                AwayTeam = new Team
                {
                    Name = "TestAway",
                    Id = Guid.NewGuid(),
                    CreatedAt = default
                }
            }, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task UpdateAsync_ShouldNotThrowException()
    {
        var homeTeam = new Team
        {
            Name = "TestHome",
            Id = default,
            CreatedAt = default
        };
        var awayTeam = new Team
        {
            Name = "TestAway",
            Id = default,
            CreatedAt = default
        };
        await _context.Teams.AddAsync(homeTeam);
        await _context.Teams.AddAsync(awayTeam);
        await _context.SaveChangesAsync();
        var match = new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
        var id = await _matchRepository.AddAsync(match, CancellationToken.None);
        var loadMatch = await _matchRepository.GetByIdAsync(id, CancellationToken.None);
        loadMatch.HomeTeamId = awayTeam.Id;
        loadMatch.AwayTeamId = homeTeam.Id;
        await _matchRepository.UpdateAsync(loadMatch, CancellationToken.None);
        
        var result = await _context.Matches.FirstAsync(x => x.Id == id);
        Assert.That(result.HomeTeamId, Is.EqualTo(awayTeam.Id));
        Assert.That(result.AwayTeamId, Is.EqualTo(homeTeam.Id));
    }
    
    
}