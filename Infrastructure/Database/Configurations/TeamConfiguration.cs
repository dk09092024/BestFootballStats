using Domain.Models;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
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
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasMany(x => x.Players)
            .WithOne()
            .HasForeignKey(player => player.TeamId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Navigation(x => x.Players)
            .AutoInclude();
    }
}