using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mandiri.MiniProject.JadwalPembelajaran.API.Migrations
{
    /// <inheritdoc />
    public partial class FixNipAndNim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NIM",
                table: "MahasiswaDTOs",
                newName: "Nim");

            migrationBuilder.RenameColumn(
                name: "NIP",
                table: "DosenDTOs",
                newName: "Nip");

            migrationBuilder.AlterColumn<string>(
                name: "Nim",
                table: "MahasiswaDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nip",
                table: "DosenDTOs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nim",
                table: "MahasiswaDTOs",
                newName: "NIM");

            migrationBuilder.RenameColumn(
                name: "Nip",
                table: "DosenDTOs",
                newName: "NIP");

            migrationBuilder.AlterColumn<string>(
                name: "NIM",
                table: "MahasiswaDTOs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NIP",
                table: "DosenDTOs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
