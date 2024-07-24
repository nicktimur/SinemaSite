using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class FİlmResim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "resim_yolu",
                table: "film",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resim_yolu",
                table: "film");
        }
    }
}
