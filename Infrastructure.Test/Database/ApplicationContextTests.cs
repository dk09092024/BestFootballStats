using Domain.Models;
using Domain.Models.Enum;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Database;
[TestFixture]
public class ApplicationContextTests
{
    private ApplicationContext _context;

    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared")
            .Options;
        _context = new ApplicationContext(options);
        await _context.Database.EnsureCreatedAsync();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
    
    [Test]
    public async Task Players_ShouldBeEmpty()
    {
        var players = await _context.Players.ToListAsync();
        Assert.That(players, Is.Empty);
    }
    
    [Test]
    public async Task Teams_ShouldBeEmpty()
    {
        var teams = await _context.Teams.ToListAsync();
        Assert.That(teams, Is.Empty);
    }
    
    [Test]
    public async Task Matches_ShouldBeEmpty()
    {
        var matches = await _context.Matches.ToListAsync();
        Assert.That(matches, Is.Empty);
    }
    
    [Test]
    public async Task Leagues_ShouldBeEmpty()
    {
        var leagues = await _context.Leagues.ToListAsync();
        Assert.That(leagues, Is.Empty);
    }
    
    [Test]
    public async Task Players_ShouldNotBeEmpty()
    {
        await _context.Players.AddAsync(new Player
        {
            Name = "Test",
            Position = (Position)1,
            Id = default,
            CreatedAt = default
        }, CancellationToken.None);
        await _context.SaveChangesAsync();
        var players = await _context.Players.ToListAsync();
        Assert.That(players, Is.Not.Empty);
    }
    
    [Test]
    public async Task Teams_ShouldNotBeEmpty()
    {
        await _context.Teams.AddAsync(new Team
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        }, CancellationToken.None);
        await _context.SaveChangesAsync();
        var teams = await _context.Teams.ToListAsync();
        Assert.That(teams, Is.Not.Empty);
    }
    
    [Test]
    public async Task Matches_ShouldNotBeEmpty()
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
        await _context.Teams.AddRangeAsync(homeTeam, awayTeam);
        await _context.SaveChangesAsync();
        await _context.Matches.AddAsync(new Match
        {
            Id = default,
            CreatedAt = default,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        }, CancellationToken.None);
        await _context.SaveChangesAsync();
        var matches = await _context.Matches.ToListAsync();
        Assert.That(matches, Is.Not.Empty);
    }
    
    [Test]
    public async Task Leagues_ShouldNotBeEmpty()
    {
        await _context.Leagues.AddAsync(new League
        {
            Name = "Test",
            Id = default,
            CreatedAt = default
        }, CancellationToken.None);
        await _context.SaveChangesAsync();
        var leagues = await _context.Leagues.ToListAsync();
        Assert.That(leagues, Is.Not.Empty);
    }
}