using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiMap.Data.Migrations
{
    public partial class addtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arealtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arealtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BygningTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BygningTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lokasjons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Selskap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntallBygg = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokasjons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Byggs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntallEtasje = table.Column<int>(type: "int", nullable: false),
                    Byggeår = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LokasjonId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Byggs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Byggs_Lokasjons_LokasjonId",
                        column: x => x.LokasjonId,
                        principalTable: "Lokasjons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etasjes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ByggId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etasjes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etasjes_Byggs_ByggId",
                        column: x => x.ByggId,
                        principalTable: "Byggs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Byggs_LokasjonId",
                table: "Byggs",
                column: "LokasjonId");

            migrationBuilder.CreateIndex(
                name: "IX_Etasjes_ByggId",
                table: "Etasjes",
                column: "ByggId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arealtypes");

            migrationBuilder.DropTable(
                name: "BygningTypes");

            migrationBuilder.DropTable(
                name: "Etasjes");

            migrationBuilder.DropTable(
                name: "Byggs");

            migrationBuilder.DropTable(
                name: "Lokasjons");
        }
    }
}
