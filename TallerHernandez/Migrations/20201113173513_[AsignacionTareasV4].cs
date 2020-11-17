using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class AsignacionTareasV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.AlterColumn<int>(
                name: "empleadoID",
                table: "AsignacionTarea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_empleadoID",
                table: "AsignacionTarea",
                column: "empleadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID",
                table: "AsignacionTarea",
                column: "empleadoID",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID",
                table: "AsignacionTarea");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoID",
                table: "AsignacionTarea");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "AsignacionTarea",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "empleadoID1",
                table: "AsignacionTarea",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_empleadoID1",
                table: "AsignacionTarea",
                column: "empleadoID1");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID1",
                table: "AsignacionTarea",
                column: "empleadoID1",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
