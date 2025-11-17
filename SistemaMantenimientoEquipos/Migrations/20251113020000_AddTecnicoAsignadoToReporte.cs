using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMantenimientoEquipos.Migrations
{
    /// <inheritdoc />
    public partial class AddTecnicoAsignadoToReporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TecnicoAsignadoId",
                table: "Reportes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_TecnicoAsignadoId",
                table: "Reportes",
                column: "TecnicoAsignadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_AspNetUsers_TecnicoAsignadoId",
                table: "Reportes",
                column: "TecnicoAsignadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_AspNetUsers_TecnicoAsignadoId",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_TecnicoAsignadoId",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TecnicoAsignadoId",
                table: "Reportes");
        }
    }
}