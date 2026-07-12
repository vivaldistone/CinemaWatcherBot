using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWatcherBot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Movies");
        }
    }
}
