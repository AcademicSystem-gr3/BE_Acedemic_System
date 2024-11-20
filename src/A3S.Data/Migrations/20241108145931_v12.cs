using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Block_ClassId",
                table: "Classes");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlockId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_BlockId",
                table: "Classes",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Block_BlockId",
                table: "Classes",
                column: "BlockId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Block_BlockId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_BlockId",
                table: "Classes");

            migrationBuilder.AlterColumn<string>(
                name: "BlockId",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Block_ClassId",
                table: "Classes",
                column: "ClassId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
