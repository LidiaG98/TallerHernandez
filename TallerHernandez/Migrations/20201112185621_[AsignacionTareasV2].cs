using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class AsignacionTareasV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Recepcion_recepcionID",
                table: "AsignacionTarea");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_recepcionID",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "recepcionID",
                table: "AsignacionTarea");

            migrationBuilder.AddColumn<int>(
                name: "procedimientoID",
                table: "AsignacionTarea",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_procedimientoID",
                table: "AsignacionTarea",
                column: "procedimientoID");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_Procedimiento_procedimientoID",
                table: "AsignacionTarea",
                column: "procedimientoID",
                principalTable: "Procedimiento",
                principalColumn: "procedimientoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Procedimiento_procedimientoID",
                table: "AsignacionTarea");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_procedimientoID",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "procedimientoID",
                table: "AsignacionTarea");

            migrationBuilder.AddColumn<int>(
                name: "recepcionID",
                table: "AsignacionTarea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_recepcionID",
                table: "AsignacionTarea",
                column: "recepcionID");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_Recepcion_recepcionID",
                table: "AsignacionTarea",
                column: "recepcionID",
                principalTable: "Recepcion",
                principalColumn: "recepcionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
