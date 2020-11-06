using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class ProcedimientosV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Mantenimiento_mantenimientoID",
                table: "Recepcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Procedimiento_procedimientoID",
                table: "Recepcion");

            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_mantenimientoID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_procedimientoID",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "mantenimientoID",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "procedimientoID",
                table: "Recepcion");

            migrationBuilder.AddColumn<int>(
                name: "recepcionID",
                table: "Procedimiento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Procedimiento_recepcionID",
                table: "Procedimiento",
                column: "recepcionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimiento_Recepcion_recepcionID",
                table: "Procedimiento",
                column: "recepcionID",
                principalTable: "Recepcion",
                principalColumn: "recepcionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedimiento_Recepcion_recepcionID",
                table: "Procedimiento");

            migrationBuilder.DropIndex(
                name: "IX_Procedimiento_recepcionID",
                table: "Procedimiento");

            migrationBuilder.DropColumn(
                name: "recepcionID",
                table: "Procedimiento");

            migrationBuilder.AddColumn<int>(
                name: "mantenimientoID",
                table: "Recepcion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "procedimientoID",
                table: "Recepcion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    mantenimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    areaID = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.mantenimientoID);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Area_areaID",
                        column: x => x.areaID,
                        principalTable: "Area",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_mantenimientoID",
                table: "Recepcion",
                column: "mantenimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_procedimientoID",
                table: "Recepcion",
                column: "procedimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_areaID",
                table: "Mantenimiento",
                column: "areaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Mantenimiento_mantenimientoID",
                table: "Recepcion",
                column: "mantenimientoID",
                principalTable: "Mantenimiento",
                principalColumn: "mantenimientoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Procedimiento_procedimientoID",
                table: "Recepcion",
                column: "procedimientoID",
                principalTable: "Procedimiento",
                principalColumn: "procedimientoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
