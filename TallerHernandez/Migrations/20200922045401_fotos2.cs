using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class fotos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagenN",
                table: "Empleado",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagenN",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagenN",
                table: "Automovil",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagenN",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "imagenN",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "imagenN",
                table: "Automovil");
        }
    }
}
