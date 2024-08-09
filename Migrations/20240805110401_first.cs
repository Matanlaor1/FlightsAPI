using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightsAPI.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    LegId = table.Column<int>(type: "int", nullable: false),
                    In = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Out = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flightId = table.Column<int>(type: "int", nullable: true),
                    CrossingTime = table.Column<int>(type: "int", nullable: false),
                    nextLegId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Legs_Flights_flightId",
                        column: x => x.flightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Legs",
                columns: new[] { "Id", "CrossingTime", "flightId", "nextLegId" },
                values: new object[,]
                {
                    { 1, 1000, null, 2 },
                    { 2, 1000, null, 3 },
                    { 3, 2000, null, 4 },
                    { 4, 8000, null, 5 },
                    { 5, 3000, null, 6 },
                    { 6, 5000, null, 7 },
                    { 7, 5000, null, 8 },
                    { 8, 2000, null, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Legs_flightId",
                table: "Legs",
                column: "flightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
