using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hunts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalDataToHint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "additionalData",
                table: "Hint",
                type: "text",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "additionalData",
                table: "Hint");
        }
    }
}
