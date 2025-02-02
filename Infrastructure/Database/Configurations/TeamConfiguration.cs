using Domain.Models;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class TeamConfiguration
{
    public static void Configure(this EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams");
        builder.HasBaseType<Entity>();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LeagueId)
            .HasColumnName("league_id")
            .HasColumnType("uuid");
        
        builder.HasMany(x => x.Players)
            .WithOne()
            .HasForeignKey(player => player.TeamId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Navigation(x => x.Players)
            .AutoInclude();
    }
}