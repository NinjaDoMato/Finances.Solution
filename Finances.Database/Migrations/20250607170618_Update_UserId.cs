using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "DateCreated");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "LastLoginAt", "LastUpdate", "PasswordHash" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 6, 7, 17, 6, 18, 171, DateTimeKind.Utc).AddTicks(8133), "daniel.deda1995@gmail.com", null, null, "$2a$11$TLwzzEQZe5QeE8anY/8pzuy.MF9n7qiNdBaVmw7L3Gvl0Dio1dD0K" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastLoginAt", "PasswordHash" },
                values: new object[] { 1, new DateTime(2025, 6, 7, 17, 2, 38, 630, DateTimeKind.Utc).AddTicks(4606), "daniel.deda1995@gmail.com", null, "$2a$11$Z7wGwK.8X5ZSr27NPzMIW.FiwWKX5xQSTW4CFYb.6RsnAggVVS7jC" });
        }
    }
}
