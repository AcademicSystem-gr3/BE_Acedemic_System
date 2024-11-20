using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class b5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "HomeworkSubmissions");

            migrationBuilder.DropColumn(
                name: "Submission",
                table: "HomeworkSubmissions");

            migrationBuilder.AlterColumn<string>(
                name: "FeedBack",
                table: "HomeworkSubmissions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "HomeworkSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "HomeworkSubmissions");

            migrationBuilder.AlterColumn<string>(
                name: "FeedBack",
                table: "HomeworkSubmissions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "HomeworkSubmissions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Submission",
                table: "HomeworkSubmissions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
