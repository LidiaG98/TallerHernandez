using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class dejamevivirunavezmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Automovil",
                table: "Automovil");

            migrationBuilder.DropColumn(
                name: "dato",
                table: "Automovil");

            migrationBuilder.AddColumn<int>(
                name: "automovilID",
                table: "Automovil",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automovil",
                table: "Automovil",
                column: "automovilID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion",
                column: "automovilID",
                principalTable: "Automovil",
                principalColumn: "automovilID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Automovil",
                table: "Automovil");

            migrationBuilder.DropColumn(
                name: "automovilID",
                table: "Automovil");

            migrationBuilder.AddColumn<int>(
                name: "dato",
                table: "Automovil",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automovil",
                table: "Automovil",
                column: "dato");

            migrationBuilder.AddForeignKey(
                name: "FK_Recepcion_Automovil_automovilID",
                table: "Recepcion",
                column: "automovilID",
                principalTable: "Automovil",
                principalColumn: "dato",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
