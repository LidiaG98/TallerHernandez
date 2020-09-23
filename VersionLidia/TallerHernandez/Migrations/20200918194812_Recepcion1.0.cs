using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class Recepcion10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Empleado",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Cliente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Automovil",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Recepcion",
                columns: table => new
                {
                    recepcionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diagnostico = table.Column<string>(maxLength: 300, nullable: false),
                    fechaEntrada = table.Column<DateTime>(nullable: false),
                    fechaSalida = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepcion", x => x.recepcionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_RecepcionID",
                table: "Empleado",
                column: "RecepcionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_RecepcionID",
                table: "Cliente",
                column: "RecepcionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automovil_RecepcionID",
                table: "Automovil",
                column: "RecepcionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Automovil_Recepcion_RecepcionID",
                table: "Automovil",
                column: "RecepcionID",
                principalTable: "Recepcion",
                principalColumn: "recepcionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Recepcion_RecepcionID",
                table: "Cliente",
                column: "RecepcionID",
                principalTable: "Recepcion",
                principalColumn: "recepcionID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Recepcion_RecepcionID",
                table: "Empleado",
                column: "RecepcionID",
                principalTable: "Recepcion",
                principalColumn: "recepcionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automovil_Recepcion_RecepcionID",
                table: "Automovil");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Recepcion_RecepcionID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Recepcion_RecepcionID",
                table: "Empleado");

            migrationBuilder.DropTable(
                name: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Empleado_RecepcionID",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_RecepcionID",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Automovil_RecepcionID",
                table: "Automovil");

            migrationBuilder.DropColumn(
                name: "RecepcionID",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "RecepcionID",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "RecepcionID",
                table: "Automovil");
        }
    }
}
