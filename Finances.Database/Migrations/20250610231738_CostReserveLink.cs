using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class CostReserveLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReserveId",
                table: "Costs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 10, 23, 17, 38, 474, DateTimeKind.Utc).AddTicks(8591), "$2a$11$HAkjDtCDfwmVmt9x.ZRfCOurJ2RpOeM4J8LdDu/FV8txXw0SmWjvu" });

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ReserveId",
                table: "Costs",
                column: "ReserveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Reserves_ReserveId",
                table: "Costs",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Reserves_ReserveId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ReserveId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ReserveId",
                table: "Costs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 7, 17, 6, 18, 171, DateTimeKind.Utc).AddTicks(8133), "$2a$11$TLwzzEQZe5QeE8anY/8pzuy.MF9n7qiNdBaVmw7L3Gvl0Dio1dD0K" });
        }
    }
}
