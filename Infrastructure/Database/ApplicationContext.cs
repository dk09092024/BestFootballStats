using Domain.Models;
using Domain.Models.Common;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<League> Leagues { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>().Configure();
        modelBuilder.Entity<League>().Configure();
        modelBuilder.Entity<Team>().Configure();
        modelBuilder.Entity<Player>().Configure();
        modelBuilder.Entity<Match>().Configure();
    }
}