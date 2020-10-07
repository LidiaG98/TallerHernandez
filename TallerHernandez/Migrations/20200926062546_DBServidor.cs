using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class DBServidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estadorespuesto",
                table: "Repuesto");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Repuesto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
