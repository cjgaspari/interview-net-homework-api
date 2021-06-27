using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeworkAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    assignmentName = table.Column<string>(type: "TEXT", nullable: true),
                    studentName = table.Column<string>(type: "TEXT", nullable: true),
                    submissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    gradingTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    grade = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    assignmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    attachmentUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Attachments_Assignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "Assignments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    assignmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notes_Assignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "Assignments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_assignmentId",
                table: "Attachments",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_assignmentId",
                table: "Notes",
                column: "assignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}
