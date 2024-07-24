using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class VizyonDurum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "film_durumu",
                table: "film",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "film_durumu",
                table: "film");
        }
    }
}
