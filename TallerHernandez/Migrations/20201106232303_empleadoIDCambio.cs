using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class empleadoIDCambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_Empleado_empleadoID",
                table: "AsignacionTarea");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoID",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "empleadoID",
                table: "Empleado");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "Recepcion",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "empleadoprototipo",
                table: "Recepcion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "prototipo",
                table: "Empleado",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "emploDUI",
                table: "Empleado",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "AsignacionTarea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "empleadoprototipo",
                table: "AsignacionTarea",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "emploDUI",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "empleadoprototipo",
                table: "AsignacionTarea");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "Recepcion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "empleadoID",
                table: "Empleado",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "AsignacionTarea",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "empleadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion",
                column: "empleadoID");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion",
                column: "empleadoID",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
