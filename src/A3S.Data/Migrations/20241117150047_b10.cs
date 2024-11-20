using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class b10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Homeworks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Homeworks");
        }
    }
}
