using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A3S.Data.Migrations
{
    /// <inheritdoc />
    public partial class b8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Lessons_SubjectId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Quizzes",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_SubjectId",
                table: "Quizzes",
                newName: "IX_Quizzes_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Quizzes",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_LessonId",
                table: "Quizzes",
                newName: "IX_Quizzes_SubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "QuizId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Lessons_SubjectId",
                table: "Quizzes",
                column: "SubjectId",
                principalTable: "Lessons",
                principalColumn: "LessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
