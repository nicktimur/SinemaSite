using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class SalonGenisletme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "satır",
                table: "tickets",
                newName: "satir");

            migrationBuilder.RenameColumn(
                name: "toplam_koltuk",
                table: "salon",
                newName: "sutun");

            migrationBuilder.AddColumn<int>(
                name: "satir",
                table: "salon",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "satir",
                table: "salon");

            migrationBuilder.RenameColumn(
                name: "satir",
                table: "tickets",
                newName: "satır");

            migrationBuilder.RenameColumn(
                name: "sutun",
                table: "salon",
                newName: "toplam_koltuk");
        }
    }
}
