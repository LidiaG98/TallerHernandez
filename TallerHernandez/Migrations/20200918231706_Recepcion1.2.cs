using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class Recepcion12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "mantenimientoID",
                table: "Recepcion",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "procedimientoID",
                table: "Recepcion",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    mantenimientoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false),
                    precio = table.Column<float>(nullable: false),
                    areaID = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Procedimiento",
                columns: table => new
                {
                    procedimientoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    procedimiento = table.Column<string>(nullable: false),
                    areaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimiento", x => x.procedimientoID);
                    table.ForeignKey(
                        name: "FK_Procedimiento_Area_areaID",
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

            migrationBuilder.CreateIndex(
                name: "IX_Procedimiento_areaID",
                table: "Procedimiento",
                column: "areaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Mantenimiento_mantenimientoID",
                table: "Recepcion",
                column: "mantenimientoID",
                principalTable: "Mantenimiento",
                principalColumn: "mantenimientoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Procedimiento_procedimientoID",
                table: "Recepcion",
                column: "procedimientoID",
                principalTable: "Procedimiento",
                principalColumn: "procedimientoID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Mantenimiento_mantenimientoID",
                table: "Recepcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Procedimiento_procedimientoID",
                table: "Recepcion");

            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "Procedimiento");

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
        }
    }
}
