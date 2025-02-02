
using Domain.Models;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class MatchConfiguration
{
    public static void Configure(this EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("matches");
        builder.HasBaseType<Entity>();

        builder.Property(x => x.HomeTeamId)
            .HasColumnName("home_team_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.AwayTeamId)
            .HasColumnName("away_team_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.TotalPasses)
            .HasColumnName("total_passes")
            .HasColumnType("int");
        
        builder.HasOne(x => x.HomeTeam)
            .WithMany()
            .HasForeignKey(x => x.HomeTeamId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.AwayTeam)
            .WithMany()
            .HasForeignKey(x => x.AwayTeamId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(x => x.HomeTeam)
            .AutoInclude();

        builder.Navigation(x => x.AwayTeam)
            .AutoInclude();
    }
}