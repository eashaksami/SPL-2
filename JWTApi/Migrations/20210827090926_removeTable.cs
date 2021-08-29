using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EBET.Migrations
{
    public partial class removeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModel");

            migrationBuilder.DropTable(
                name: "CoursesModel");

            migrationBuilder.DropTable(
                name: "QuestionModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseModel",
                columns: table => new
                {
                    CourseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Length = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CoursesModel",
                columns: table => new
                {
                    CourseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QuestionModel",
                columns: table => new
                {
                    AnswerDetails = table.Column<string>(type: "TEXT", nullable: true),
                    ChapterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "TEXT", nullable: true),
                    Option1 = table.Column<string>(type: "TEXT", nullable: true),
                    Option2 = table.Column<string>(type: "TEXT", nullable: true),
                    Option3 = table.Column<string>(type: "TEXT", nullable: true),
                    Option4 = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    question = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
