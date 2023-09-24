using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCollumnCostPercentages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostPayer");

            migrationBuilder.AddColumn<decimal>(
                name: "CassiaPercentage",
                table: "Costs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DanielPercentage",
                table: "Costs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CassiaPercentage",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "DanielPercentage",
                table: "Costs");

            migrationBuilder.CreateTable(
                name: "CostPayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AccountUser = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PaymentPercentage = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostPayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostPayer_Costs_CostId",
                        column: x => x.CostId,
                        principalTable: "Costs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CostPayer_CostId",
                table: "CostPayer",
                column: "CostId");
        }
    }
}
