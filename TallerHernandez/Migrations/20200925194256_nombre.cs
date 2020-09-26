using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class nombre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Repuesto",
                columns: table => new
                {
                    repuestoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: false),
                    categoria = table.Column<string>(nullable: false),
                    anio = table.Column<int>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    tipo = table.Column<string>(nullable: false),
                    estadorespuesto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuesto", x => x.repuestoID);
                });

         }

    }
}
