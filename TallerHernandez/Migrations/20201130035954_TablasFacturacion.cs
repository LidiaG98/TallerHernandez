using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerHernandez.Migrations
{
    public partial class TablasFacturacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    facturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRecepcion = table.Column<int>(nullable: false),
                    recepcionID = table.Column<int>(nullable: true),
                    idCliente = table.Column<int>(nullable: false),
                    clienteID = table.Column<string>(nullable: true),
                    impuesto = table.Column<double>(nullable: false),
                    total = table.Column<double>(nullable: false),
                    totalNeto = table.Column<double>(nullable: false),
                    fechaEmision = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.facturaId);
                    table.ForeignKey(
                        name: "FK_Factura_Cliente_clienteID",
                        column: x => x.clienteID,
                        principalTable: "Cliente",
                        principalColumn: "clienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factura_Recepcion_recepcionID",
                        column: x => x.recepcionID,
                        principalTable: "Recepcion",
                        principalColumn: "recepcionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Extra",
                columns: table => new
                {
                    idExtra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAsignacionTarea = table.Column<int>(nullable: false),
                    asignacionTareaID = table.Column<int>(nullable: true),
                    idFactura = table.Column<int>(nullable: false),
                    facturaId = table.Column<int>(nullable: true),
                    total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extra", x => x.idExtra);
                    table.ForeignKey(
                        name: "FK_Extra_AsignacionTarea_asignacionTareaID",
                        column: x => x.asignacionTareaID,
                        principalTable: "AsignacionTarea",
                        principalColumn: "asignacionTareaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Extra_Factura_facturaId",
                        column: x => x.facturaId,
                        principalTable: "Factura",
                        principalColumn: "facturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extra_asignacionTareaID",
                table: "Extra",
                column: "asignacionTareaID");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_facturaId",
                table: "Extra",
                column: "facturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_clienteID",
                table: "Factura",
                column: "clienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_recepcionID",
                table: "Factura",
                column: "recepcionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropTable(
                name: "Factura");
        }
    }
}
