using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketApi.Migrations
{
    /// <inheritdoc />
    public partial class InitSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AwayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AwayScore = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HomeFouls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AwayFouls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    QuarterDurationSec = table.Column<int>(type: "int", nullable: false, defaultValue: 600),
                    QuartersPlayed = table.Column<int>(type: "int", nullable: false, defaultValue: 4),
                    HomeColorHex = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    AwayColorHex = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    EndedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ExtraJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchResults");
        }
    }
}
