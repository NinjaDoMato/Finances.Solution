using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaMensal",
                table: "Reserves",
                newName: "MonthlyGoal");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 16, 23, 25, 27, 388, DateTimeKind.Utc).AddTicks(8656), "$2a$11$czfARIatZ5G5.et1KVFunuKJh0DukeE3alxNNT.tqpKaGqOMrscii" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyGoal",
                table: "Reserves",
                newName: "MetaMensal");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 16, 23, 23, 33, 608, DateTimeKind.Utc).AddTicks(229), "$2a$11$xVKcal5tAqQpPuAcAOIxF.BovdlUKYkC.5A3k6e6tLV/rtkAdr/Fq" });
        }
    }
}
