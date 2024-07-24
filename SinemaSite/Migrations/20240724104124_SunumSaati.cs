using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class SunumSaati : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "sunum_tarihi",
                table: "gosterim",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "sunum_saati",
                table: "gosterim",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sunum_saati",
                table: "gosterim");

            migrationBuilder.AlterColumn<DateTime>(
                name: "sunum_tarihi",
                table: "gosterim",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
