using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveInvestmentMaps_Investments_InvestmentId",
                table: "ReserveInvestmentMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveInvestmentMaps_Reserves_ReserveId",
                table: "ReserveInvestmentMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveInvestmentMaps",
                table: "ReserveInvestmentMaps");

            migrationBuilder.RenameTable(
                name: "ReserveInvestmentMaps",
                newName: "ReserveInvestmentsMaps");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveInvestmentMaps_InvestmentId",
                table: "ReserveInvestmentsMaps",
                newName: "IX_ReserveInvestmentsMaps_InvestmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveInvestmentsMaps",
                table: "ReserveInvestmentsMaps",
                columns: new[] { "ReserveId", "InvestmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveInvestmentsMaps_Investments_InvestmentId",
                table: "ReserveInvestmentsMaps",
                column: "InvestmentId",
                principalTable: "Investments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveInvestmentsMaps_Reserves_ReserveId",
                table: "ReserveInvestmentsMaps",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveInvestmentsMaps_Investments_InvestmentId",
                table: "ReserveInvestmentsMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveInvestmentsMaps_Reserves_ReserveId",
                table: "ReserveInvestmentsMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveInvestmentsMaps",
                table: "ReserveInvestmentsMaps");

            migrationBuilder.RenameTable(
                name: "ReserveInvestmentsMaps",
                newName: "ReserveInvestmentMaps");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveInvestmentsMaps_InvestmentId",
                table: "ReserveInvestmentMaps",
                newName: "IX_ReserveInvestmentMaps_InvestmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveInvestmentMaps",
                table: "ReserveInvestmentMaps",
                columns: new[] { "ReserveId", "InvestmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveInvestmentMaps_Investments_InvestmentId",
                table: "ReserveInvestmentMaps",
                column: "InvestmentId",
                principalTable: "Investments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveInvestmentMaps_Reserves_ReserveId",
                table: "ReserveInvestmentMaps",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
