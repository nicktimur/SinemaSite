using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class FilmTurler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tur",
                table: "film",
                newName: "Turler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Turler",
                table: "film",
                newName: "Tur");
        }
    }
}
