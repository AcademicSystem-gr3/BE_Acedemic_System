using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommentBlogs_BlogID",
                table: "CommentBlogs",
                column: "BlogID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs");

            migrationBuilder.DropIndex(
                name: "IX_CommentBlogs_BlogID",
                table: "CommentBlogs");
        }
    }
}
