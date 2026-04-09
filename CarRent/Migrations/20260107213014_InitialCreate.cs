using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRent.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RentalType = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "tblCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCars",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCars", x => new { x.CustomerId, x.CarId });
                    table.ForeignKey(
                        name: "FK_CustomerCars_tblCars_CarId",
                        column: x => x.CarId,
                        principalTable: "tblCars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCars_tblCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tblCustomers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_tblRentals_tblCars_CarId",
                        column: x => x.CarId,
                        principalTable: "tblCars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRentals_tblCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tblCustomers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCars",
                columns: new[] { "CarId", "Brand", "Model", "PricePerDay", "RentalType", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", "Corolla", 30.0, 0, 2020 },
                    { 2, "Hyundai", "Elantra", 35.0, 0, 2021 },
                    { 3, "BMW", "X3", 60.0, 1, 2019 },
                    { 4, "Mercedes", "C200", 120.0, 2, 2022 }
                });

            migrationBuilder.InsertData(
                table: "tblCustomers",
                columns: new[] { "CustomerId", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, "alice@email.com", "Alice" },
                    { 2, "bob@email.com", "Bob" }
                });

            migrationBuilder.InsertData(
                table: "CustomerCars",
                columns: new[] { "CarId", "CustomerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCars_CarId",
                table: "CustomerCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRentals_CarId",
                table: "tblRentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRentals_CustomerId",
                table: "tblRentals",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCars");

            migrationBuilder.DropTable(
                name: "tblRentals");

            migrationBuilder.DropTable(
                name: "tblCars");

            migrationBuilder.DropTable(
                name: "tblCustomers");
        }
    }
}
