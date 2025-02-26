﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Domain.Models.Common.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("Entity");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.League", b =>
                {
                    b.HasBaseType("Domain.Models.Common.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.ToTable("leagues", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Match", b =>
                {
                    b.HasBaseType("Domain.Models.Common.Entity");

                    b.Property<Guid>("AwayTeamId")
                        .HasColumnType("uuid")
                        .HasColumnName("away_team_id");

                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uuid")
                        .HasColumnName("home_team_id");

                    b.Property<int>("TotalPasses")
                        .HasColumnType("int")
                        .HasColumnName("total_passes");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("matches", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Player", b =>
                {
                    b.HasBaseType("Domain.Models.Common.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("position");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uuid")
                        .HasColumnName("team_id");

                    b.HasIndex("TeamId");

                    b.ToTable("players", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.HasBaseType("Domain.Models.Common.Entity");

                    b.Property<Guid?>("LeagueId")
                        .HasColumnType("uuid")
                        .HasColumnName("league_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasIndex("LeagueId");

                    b.ToTable("teams", (string)null);
                });

            modelBuilder.Entity("Domain.Models.League", b =>
                {
                    b.HasOne("Domain.Models.Common.Entity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Models.League", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Match", b =>
                {
                    b.HasOne("Domain.Models.Team", "AwayTeam")
                        .WithMany()
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Domain.Models.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Domain.Models.Common.Entity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Models.Match", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");
                });

            modelBuilder.Entity("Domain.Models.Player", b =>
                {
                    b.HasOne("Domain.Models.Common.Entity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Models.Player", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.HasOne("Domain.Models.Common.Entity", null)
                        .WithOne()
                        .HasForeignKey("Domain.Models.Team", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.League", null)
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Domain.Models.League", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Domain.Models.Team", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
