using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DosenDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIP = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosenDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GedungDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GedungDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KelasDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KelasDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MataKuliahDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Sks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MataKuliahDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuanganDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    IdGedung = table.Column<int>(type: "int", nullable: false),
                    GedungId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuanganDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuanganDTOs_GedungDTOs_GedungId",
                        column: x => x.GedungId,
                        principalTable: "GedungDTOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MahasiswaDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    IdKelas = table.Column<int>(type: "int", nullable: false),
                    YearOfStudy = table.Column<int>(type: "int", nullable: false),
                    KelasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MahasiswaDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MahasiswaDTOs_KelasDTOs_KelasId",
                        column: x => x.KelasId,
                        principalTable: "KelasDTOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JadwalDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDosen = table.Column<int>(type: "int", nullable: false),
                    IdRuangan = table.Column<int>(type: "int", nullable: false),
                    IdKelas = table.Column<int>(type: "int", nullable: false),
                    IdMataKuliah = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosenDTOId = table.Column<int>(type: "int", nullable: true),
                    MahasiswaDTOId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JadwalDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JadwalDTOs_DosenDTOs_DosenDTOId",
                        column: x => x.DosenDTOId,
                        principalTable: "DosenDTOs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JadwalDTOs_MahasiswaDTOs_MahasiswaDTOId",
                        column: x => x.MahasiswaDTOId,
                        principalTable: "MahasiswaDTOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JadwalDTOs_DosenDTOId",
                table: "JadwalDTOs",
                column: "DosenDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_JadwalDTOs_MahasiswaDTOId",
                table: "JadwalDTOs",
                column: "MahasiswaDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_MahasiswaDTOs_KelasId",
                table: "MahasiswaDTOs",
                column: "KelasId");

            migrationBuilder.CreateIndex(
                name: "IX_RuanganDTOs_GedungId",
                table: "RuanganDTOs",
                column: "GedungId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JadwalDTOs");

            migrationBuilder.DropTable(
                name: "MataKuliahDTOs");

            migrationBuilder.DropTable(
                name: "RuanganDTOs");

            migrationBuilder.DropTable(
                name: "DosenDTOs");

            migrationBuilder.DropTable(
                name: "MahasiswaDTOs");

            migrationBuilder.DropTable(
                name: "GedungDTOs");

            migrationBuilder.DropTable(
                name: "KelasDTOs");
        }
    }
}
