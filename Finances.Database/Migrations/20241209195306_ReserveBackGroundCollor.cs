using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Database.Migrations
{
    /// <inheritdoc />
    public partial class ReserveBackGroundCollor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayColor",
                table: "Reserves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayColor",
                table: "Reserves");
        }
    }
}
