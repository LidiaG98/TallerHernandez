using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class empleadoIDCambio1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoprototipo",
                table: "AsignacionTarea");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoprototipo",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoprototipo",
                table: "Recepcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoprototipo",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "empleadoprototipo",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "prototipo",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "empleadoprototipo",
                table: "AsignacionTarea");

            migrationBuilder.AddColumn<int>(
                name: "empleadoID1",
                table: "Recepcion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "empleadoID",
                table: "Empleado",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "empleadoID1",
                table: "AsignacionTarea",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "empleadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoID1",
                table: "Recepcion",
                column: "empleadoID1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID1",
                table: "Recepcion",
                column: "empleadoID1",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID1",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoID1",
                table: "Recepcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "empleadoID1",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "empleadoID",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "empleadoID1",
                table: "AsignacionTarea");

            migrationBuilder.AddColumn<int>(
                name: "empleadoprototipo",
                table: "Recepcion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "prototipo",
                table: "Empleado",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "empleadoprototipo",
                table: "AsignacionTarea",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "prototipo");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoprototipo",
                table: "Recepcion",
                column: "empleadoprototipo");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_empleadoprototipo",
                table: "AsignacionTarea",
                column: "empleadoprototipo");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoprototipo",
                table: "AsignacionTarea",
                column: "empleadoprototipo",
                principalTable: "Empleado",
                principalColumn: "prototipo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoprototipo",
                table: "Recepcion",
                column: "empleadoprototipo",
                principalTable: "Empleado",
                principalColumn: "prototipo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
