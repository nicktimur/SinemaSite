using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class bakiyeveucret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "film",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isim = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sure = table.Column<int>(type: "int", nullable: true),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "kullanici",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isim = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    soyisim = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kullanici_adi = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sifre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kullanici_tipi = table.Column<int>(type: "int", nullable: false),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    aktif_mi = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    son_aktif_tarih = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "sinema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isim = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    konum = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "salon",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    toplam_koltuk = table.Column<int>(type: "int", nullable: true),
                    salon_mumarasi = table.Column<int>(type: "int", nullable: true),
                    salon_tipi = table.Column<string>(type: "enum('Standart','IMAX','4DX')", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sinema_id = table.Column<long>(type: "bigint", nullable: true),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "salon_ibfk_1",
                        column: x => x.sinema_id,
                        principalTable: "sinema",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "gosterim",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    salon_id = table.Column<long>(type: "bigint", nullable: true),
                    film_id = table.Column<long>(type: "bigint", nullable: true),
                    sunum_tarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "gosterim_ibfk_1",
                        column: x => x.salon_id,
                        principalTable: "salon",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "gosterim_ibfk_2",
                        column: x => x.film_id,
                        principalTable: "film",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    gosterim_id = table.Column<long>(type: "bigint", nullable: true),
                    sutun = table.Column<int>(type: "int", nullable: true),
                    satır = table.Column<int>(type: "int", nullable: true),
                    musteri_id = table.Column<long>(type: "bigint", nullable: true),
                    satin_alim_tarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    olusturulma_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    silinme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true),
                    guncelleme_tarihi = table.Column<DateTime>(type: "timestamp", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "tickets_ibfk_1",
                        column: x => x.gosterim_id,
                        principalTable: "gosterim",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "tickets_ibfk_2",
                        column: x => x.musteri_id,
                        principalTable: "kullanici",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "film_id",
                table: "gosterim",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "salon_id",
                table: "gosterim",
                column: "salon_id");

            migrationBuilder.CreateIndex(
                name: "email",
                table: "kullanici",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "kullanici_adi",
                table: "kullanici",
                column: "kullanici_adi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "sinema_id",
                table: "salon",
                column: "sinema_id");

            migrationBuilder.CreateIndex(
                name: "gosterim_id",
                table: "tickets",
                column: "gosterim_id");

            migrationBuilder.CreateIndex(
                name: "musteri_id",
                table: "tickets",
                column: "musteri_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "gosterim");

            migrationBuilder.DropTable(
                name: "kullanici");

            migrationBuilder.DropTable(
                name: "salon");

            migrationBuilder.DropTable(
                name: "film");

            migrationBuilder.DropTable(
                name: "sinema");
        }
    }
}
