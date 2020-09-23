using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class Recepciones11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "automovilID",
                table: "Recepcion",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "clienteID",
                table: "Recepcion",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "empleadoID",
                table: "Recepcion",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_automovilID",
                table: "Recepcion",
                column: "automovilID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_clienteID",
                table: "Recepcion",
                column: "clienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion",
                column: "empleadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion",
                column: "automovilID",
                principalTable: "Automovil",
                principalColumn: "automovilID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Cliente_clienteID",
                table: "Recepcion",
                column: "clienteID",
                principalTable: "Cliente",
                principalColumn: "clienteID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion",
                column: "empleadoID",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Cliente_clienteID",
                table: "Recepcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_automovilID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_clienteID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "automovilID",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "clienteID",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "empleadoID",
                table: "Recepcion");

            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Empleado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecepcionID",
                table: "Automovil",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
