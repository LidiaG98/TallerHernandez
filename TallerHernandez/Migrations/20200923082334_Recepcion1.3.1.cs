using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class Recepcion131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<float>(
                name: "precio",
                table: "Procedimiento",
                nullable: false,
                defaultValue: 0f);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
                migrationBuilder.DropColumn(
                name: "precio",
                table: "Procedimiento");
            
        }
    }
}
