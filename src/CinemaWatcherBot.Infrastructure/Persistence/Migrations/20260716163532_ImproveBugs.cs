using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWatcherBot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImproveBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "MovieSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "MovieSessions");
        }
    }
}
