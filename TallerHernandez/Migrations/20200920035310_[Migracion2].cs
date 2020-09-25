using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class Migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionTarea",
                columns: table => new
                {
                    asignacionTareaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estadoTarea = table.Column<bool>(nullable: false),
                    recepcionID = table.Column<int>(nullable: false),
                    empleadoID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionTarea", x => x.asignacionTareaID);
                    table.ForeignKey(
                        name: "FK_AsignacionTarea_Empleado_empleadoID",
                        column: x => x.empleadoID,
                        principalTable: "Empleado",
                        principalColumn: "empleadoID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AsignacionTarea_Recepcion_recepcionID",
                        column: x => x.recepcionID,
                        principalTable: "Recepcion",
                        principalColumn: "recepcionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_empleadoID",
                table: "AsignacionTarea",
                column: "empleadoID");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTarea_recepcionID",
                table: "AsignacionTarea",
                column: "recepcionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionTarea");
        }
    }
}
