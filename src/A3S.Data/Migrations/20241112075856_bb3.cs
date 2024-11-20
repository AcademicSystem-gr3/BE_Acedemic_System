using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class bb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBlogs_Blogs_BlogID",
                table: "CommentBlogs",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "BlogId");
        }
    }
}
