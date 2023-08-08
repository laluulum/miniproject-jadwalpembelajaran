using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Migrations
{
    /// <inheritdoc />
    public partial class FixJadwalDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JadwalDTOs_DosenDTOs_DosenDTOId",
                table: "JadwalDTOs");

            migrationBuilder.RenameColumn(
                name: "DosenDTOId",
                table: "JadwalDTOs",
                newName: "RuanganId");

            migrationBuilder.RenameIndex(
                name: "IX_JadwalDTOs_DosenDTOId",
                table: "JadwalDTOs",
                newName: "IX_JadwalDTOs_RuanganId");

            migrationBuilder.AddColumn<int>(
                name: "DosenId",
                table: "JadwalDTOs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KelasId",
                table: "JadwalDTOs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MataKuliahId",
                table: "JadwalDTOs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JadwalDTOs_DosenId",
                table: "JadwalDTOs",
                column: "DosenId");

            migrationBuilder.CreateIndex(
                name: "IX_JadwalDTOs_KelasId",
                table: "JadwalDTOs",
                column: "KelasId");

            migrationBuilder.CreateIndex(
                name: "IX_JadwalDTOs_MataKuliahId",
                table: "JadwalDTOs",
                column: "MataKuliahId");

            migrationBuilder.AddForeignKey(
                name: "FK_JadwalDTOs_DosenDTOs_DosenId",
                table: "JadwalDTOs",
                column: "DosenId",
                principalTable: "DosenDTOs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JadwalDTOs_KelasDTOs_KelasId",
                table: "JadwalDTOs",
                column: "KelasId",
                principalTable: "KelasDTOs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JadwalDTOs_MataKuliahDTOs_MataKuliahId",
                table: "JadwalDTOs",
                column: "MataKuliahId",
                principalTable: "MataKuliahDTOs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JadwalDTOs_RuanganDTOs_RuanganId",
                table: "JadwalDTOs",
                column: "RuanganId",
                principalTable: "RuanganDTOs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JadwalDTOs_DosenDTOs_DosenId",
                table: "JadwalDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_JadwalDTOs_KelasDTOs_KelasId",
                table: "JadwalDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_JadwalDTOs_MataKuliahDTOs_MataKuliahId",
                table: "JadwalDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_JadwalDTOs_RuanganDTOs_RuanganId",
                table: "JadwalDTOs");

            migrationBuilder.DropIndex(
                name: "IX_JadwalDTOs_DosenId",
                table: "JadwalDTOs");

            migrationBuilder.DropIndex(
                name: "IX_JadwalDTOs_KelasId",
                table: "JadwalDTOs");

            migrationBuilder.DropIndex(
                name: "IX_JadwalDTOs_MataKuliahId",
                table: "JadwalDTOs");

            migrationBuilder.DropColumn(
                name: "DosenId",
                table: "JadwalDTOs");

            migrationBuilder.DropColumn(
                name: "KelasId",
                table: "JadwalDTOs");

            migrationBuilder.DropColumn(
                name: "MataKuliahId",
                table: "JadwalDTOs");

            migrationBuilder.RenameColumn(
                name: "RuanganId",
                table: "JadwalDTOs",
                newName: "DosenDTOId");

            migrationBuilder.RenameIndex(
                name: "IX_JadwalDTOs_RuanganId",
                table: "JadwalDTOs",
                newName: "IX_JadwalDTOs_DosenDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_JadwalDTOs_DosenDTOs_DosenDTOId",
                table: "JadwalDTOs",
                column: "DosenDTOId",
                principalTable: "DosenDTOs",
                principalColumn: "Id");
        }
    }
}
