using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class SalonNumarasiFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "konum",
                table: "sinema",
                newName: "adres");

            migrationBuilder.RenameColumn(
                name: "salon_mumarasi",
                table: "salon",
                newName: "salon_numarasi");

            migrationBuilder.AlterColumn<int>(
                name: "salon_tipi",
                table: "salon",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Standart','IMAX','4DX')")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "adres",
                table: "sinema",
                newName: "konum");

            migrationBuilder.RenameColumn(
                name: "salon_numarasi",
                table: "salon",
                newName: "salon_mumarasi");

            migrationBuilder.AlterColumn<string>(
                name: "salon_tipi",
                table: "salon",
                type: "enum('Standart','IMAX','4DX')",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
