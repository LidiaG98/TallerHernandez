using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class fer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "empleadoprogresoID",
                table: "AsignacionTarea",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmpleadoProgreso",
                columns: table => new
                {
                    empleadoprogresoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empleadoID = table.Column<int>(nullable: true),
                    actTerminadas = table.Column<int>(nullable: false),
                    actSinTerminar = table.Column<int>(nullable: false),
                    porcentajeLogrado = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoProgreso", x => x.empleadoprogresoID);
                    table.ForeignKey(
                        name: "FK_EmpleadoProgreso_Empleado_empleadoID",
                        column: x => x.empleadoID,
                        principalTable: "Empleado",
                        principalColumn: "empleadoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_empleadoprogresoID",
                table: "AsignacionTarea",
                column: "empleadoprogresoID");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProgreso_empleadoID",
                table: "EmpleadoProgreso",
                column: "empleadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTarea_EmpleadoProgreso_empleadoprogresoID",
                table: "AsignacionTarea",
                column: "empleadoprogresoID",
                principalTable: "EmpleadoProgreso",
                principalColumn: "empleadoprogresoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTarea_EmpleadoProgreso_empleadoprogresoID",
                table: "AsignacionTarea");

            migrationBuilder.DropTable(
                name: "EmpleadoProgreso");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTarea_empleadoprogresoID",
                table: "AsignacionTarea");

            migrationBuilder.DropColumn(
                name: "empleadoprogresoID",
                table: "AsignacionTarea");
        }
    }
}
