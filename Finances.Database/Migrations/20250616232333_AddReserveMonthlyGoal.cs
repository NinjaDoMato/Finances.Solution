using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddReserveMonthlyGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MetaMensal",
                table: "Reserves",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 16, 23, 23, 33, 608, DateTimeKind.Utc).AddTicks(229), "$2a$11$xVKcal5tAqQpPuAcAOIxF.BovdlUKYkC.5A3k6e6tLV/rtkAdr/Fq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaMensal",
                table: "Reserves");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 10, 23, 17, 38, 474, DateTimeKind.Utc).AddTicks(8591), "$2a$11$HAkjDtCDfwmVmt9x.ZRfCOurJ2RpOeM4J8LdDu/FV8txXw0SmWjvu" });
        }
    }
}
