using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EBET.Migrations
{
    public partial class addNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageIdForAnswer",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseModel",
                columns: table => new
                {
                    CourseCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Length = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CoursesModel",
                columns: table => new
                {
                    CourseCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QuestionModel",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false),
                    ChapterId = table.Column<int>(nullable: false),
                    question = table.Column<string>(nullable: true),
                    Option1 = table.Column<string>(nullable: true),
                    Option2 = table.Column<string>(nullable: true),
                    Option3 = table.Column<string>(nullable: true),
                    Option4 = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    AnswerDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModel");

            migrationBuilder.DropTable(
                name: "CoursesModel");

            migrationBuilder.DropTable(
                name: "QuestionModel");

            migrationBuilder.DropColumn(
                name: "ImageIdForAnswer",
                table: "Questions");
        }
    }
}
