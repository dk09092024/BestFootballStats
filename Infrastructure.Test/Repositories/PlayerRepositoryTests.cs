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
public class PlayerRepositoryTests
{
    private PlayerRepository _playerRepository;
    private Mock<ITeamRepository> _teamRepository;
    private ApplicationContext _context;
 
    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared")
            .Options;
        _context = new ApplicationContext(options);
        await _context.Database.EnsureCreatedAsync();
        _teamRepository = new Mock<ITeamRepository>();
        _playerRepository = new PlayerRepository(_context,_teamRepository.Object);
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
        Assert.ThrowsAsync<EntityDoesNotExistExeption<PlayerRepository, Player>>(async () =>
        {
            await _playerRepository.MustExistAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task MustExistAsync_ShouldNotThrowException()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        await _playerRepository.MustExistAsync(player.Id, CancellationToken.None);
    }
    
    [Test]
    public async Task AddAsync_ShouldReturnId()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        var id = await _playerRepository.AddAsync(player, CancellationToken.None);
        Assert.That(id, Is.Not.EqualTo(default(Guid)));
    }
    
    [Test]
    public async Task UpdateAsync_ShouldUpdate()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        var id = await _playerRepository.AddAsync(player, CancellationToken.None);
        player.Name = "Test2";
        await _playerRepository.UpdateAsync(player, CancellationToken.None);
        var updatedPlayer = await _context.Players.FindAsync(id);
        Assert.That(updatedPlayer.Name, Is.EqualTo("Test2"));
    }
    
    [Test]
    public async Task UpdateAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<PlayerRepository, Player>>(async () =>
        {
            await _playerRepository.UpdateAsync(new Player
            {
                Name = "Test",
                Id = Guid.NewGuid(),
                CreatedAt = default,
                Position = (Position)1,
            }, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task DeleteAsync_ShouldDelete()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        var id = await _playerRepository.AddAsync(player, CancellationToken.None);
        await _playerRepository.DeleteAsync(id, CancellationToken.None);
        var deletedPlayer = await _context.Players.FindAsync(id);
        Assert.That(deletedPlayer, Is.Null);
    }
    
    [Test]
    public async Task DeleteAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<PlayerRepository, Player>>(async () =>
        {
            await _playerRepository.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
    
    [Test]
    public async Task LinkToTeam_ShouldLink()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        var team = new Team
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
        };
        await _context.Players.AddAsync(player);
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();
        await _playerRepository.LinkToTeam(player.Id, team.Id, CancellationToken.None);
        var linkedPlayer = await _context.Players.FindAsync(player.Id);
        Assert.That(linkedPlayer.TeamId, Is.EqualTo(team.Id));
    }
    
    [Test]
    public async Task LinkToTeam_ShouldThrowException_Player()
    {
        var team = new Team
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
        };
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();
        Assert.ThrowsAsync<EntityDoesNotExistExeption<PlayerRepository, Player>>(async () =>
        {
            await _playerRepository.LinkToTeam(Guid.NewGuid(), team.Id, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task LinkToTeam_ShouldThrowException_Team()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        
        var wrongTeamId = Guid.NewGuid();
        
        _teamRepository.Setup(x => x.MustExistAsync(wrongTeamId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityDoesNotExistExeption<TeamRepository, Team>(wrongTeamId));
        
        Assert.ThrowsAsync<EntityDoesNotExistExeption<TeamRepository, Team>>(async () =>
        {
            await _playerRepository.LinkToTeam(player.Id, wrongTeamId, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnTrue()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        var result = await _playerRepository.ExistsAsync(player.Id, CancellationToken.None);
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task ExistsAsync_ShouldReturnFalse()
    {
        var result = await _playerRepository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldReturnPlayer()
    {
        var player = new Player
        {
            Name = "Test",
            Id = default,
            CreatedAt = default,
            Position = (Position)1,
        };
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        var result = await _playerRepository.GetByIdAsync(player.Id, CancellationToken.None);
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public async Task GetByIdAsync_ShouldThrowException()
    {
        Assert.ThrowsAsync<EntityDoesNotExistExeption<PlayerRepository, Player>>(async () =>
        {
            await _playerRepository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        });
    }
}