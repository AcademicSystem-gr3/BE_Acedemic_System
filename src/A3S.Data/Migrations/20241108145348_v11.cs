using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockId",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Block_ClassId",
                table: "Classes",
                column: "ClassId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Block_ClassId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Classes");
        }
    }
}
