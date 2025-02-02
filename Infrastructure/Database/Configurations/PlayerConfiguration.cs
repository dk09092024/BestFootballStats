using Domain.Models;
using Domain.Models.Common;
using Infrastructure.Database.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class PlayerConfiguration 
{
    public static void Configure(this EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        builder.HasBaseType<Entity>();
        builder.HasIndex(x => x.TeamId);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Position)
            .HasColumnName("position")
            .HasColumnType("varchar")
            .HasConversion<PositionConversion>()
            .IsRequired();

        builder.Property(x => x.TeamId)
            .HasColumnName("team_id")
            .HasColumnType("uuid");
    }
}