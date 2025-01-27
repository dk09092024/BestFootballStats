using Domain.Models;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class LeagueConfiguration : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        builder.ToTable("leagues");
        builder.HasBaseType<Entity>();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasMany(x => x.Teams)
            .WithOne()
            .HasForeignKey(team => team.LeagueId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(x => x.Teams)
            .AutoInclude();
    }
}