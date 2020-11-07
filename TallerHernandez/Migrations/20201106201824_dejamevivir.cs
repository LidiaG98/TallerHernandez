using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class dejamevivir : Migration
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
                name: "automovilID",
                table: "Automovil");

            migrationBuilder.AlterColumn<int>(
                name: "automovilID",
                table: "Recepcion",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "dato",
                table: "Automovil",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "placa",
                table: "Automovil");

            migrationBuilder.AlterColumn<string>(
                name: "automovilID",
                table: "Recepcion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "automovilID",
                table: "Automovil",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
    }
}
