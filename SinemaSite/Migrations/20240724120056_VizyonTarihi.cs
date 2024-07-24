using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class VizyonTarihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "vizyon_tarihi",
                table: "film",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vizyon_tarihi",
                table: "film");
        }
    }
}
