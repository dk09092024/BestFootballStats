using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BestFootballStatsApp");

            migrationBuilder.CreateTable(
                name: "Entity",
                schema: "BestFootballStatsApp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "leagues",
                schema: "BestFootballStatsApp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leagues", x => x.id);
                    table.ForeignKey(
                        name: "FK_leagues_Entity_id",
                        column: x => x.id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "Entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "BestFootballStatsApp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    league_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_teams_Entity_id",
                        column: x => x.id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "Entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teams_leagues_league_id",
                        column: x => x.league_id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "BestFootballStatsApp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    away_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_passes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.id);
                    table.ForeignKey(
                        name: "FK_matches_Entity_id",
                        column: x => x.id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "Entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_teams_away_team_id",
                        column: x => x.away_team_id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_matches_teams_home_team_id",
                        column: x => x.home_team_id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "players",
                schema: "BestFootballStatsApp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    position = table.Column<string>(type: "varchar", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                    table.ForeignKey(
                        name: "FK_players_Entity_id",
                        column: x => x.id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "Entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_players_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "BestFootballStatsApp",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_away_team_id",
                schema: "BestFootballStatsApp",
                table: "matches",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_home_team_id",
                schema: "BestFootballStatsApp",
                table: "matches",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_team_id",
                schema: "BestFootballStatsApp",
                table: "players",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_league_id",
                schema: "BestFootballStatsApp",
                table: "teams",
                column: "league_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "BestFootballStatsApp");

            migrationBuilder.DropTable(
                name: "players",
                schema: "BestFootballStatsApp");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "BestFootballStatsApp");

            migrationBuilder.DropTable(
                name: "leagues",
                schema: "BestFootballStatsApp");

            migrationBuilder.DropTable(
                name: "Entity",
                schema: "BestFootballStatsApp");
        }
    }
}
