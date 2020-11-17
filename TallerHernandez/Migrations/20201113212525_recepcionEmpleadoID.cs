using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class recepcionEmpleadoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID1",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoID1",
                table: "Recepcion");

            migrationBuilder.DropColumn(
                name: "empleadoID1",
                table: "Recepcion");

            migrationBuilder.AlterColumn<int>(
                name: "empleadoID",
                table: "Recepcion",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion",
                column: "empleadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion",
                column: "empleadoID",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID",
                table: "Recepcion");

            migrationBuilder.DropIndex(
                name: "IX_Recepcion_empleadoID",
                table: "Recepcion");

            migrationBuilder.AlterColumn<string>(
                name: "empleadoID",
                table: "Recepcion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "empleadoID1",
                table: "Recepcion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_empleadoID1",
                table: "Recepcion",
                column: "empleadoID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Empleado_empleadoID1",
                table: "Recepcion",
                column: "empleadoID1",
                principalTable: "Empleado",
                principalColumn: "empleadoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
