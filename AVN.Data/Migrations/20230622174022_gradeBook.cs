using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class gradeBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlForm",
                table: "GradeBooks");

            migrationBuilder.DropColumn(
                name: "CreditsAmount",
                table: "GradeBooks");

            migrationBuilder.RenameColumn(
                name: "SyllabusHours",
                table: "GradeBooks",
                newName: "AcademicYearId");

            migrationBuilder.AddColumn<int>(
                name: "ControlForm",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GradeBooks_AcademicYearId",
                table: "GradeBooks",
                column: "AcademicYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeBooks_AcademicYears_AcademicYearId",
                table: "GradeBooks",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeBooks_AcademicYears_AcademicYearId",
                table: "GradeBooks");

            migrationBuilder.DropIndex(
                name: "IX_GradeBooks_AcademicYearId",
                table: "GradeBooks");

            migrationBuilder.DropColumn(
                name: "ControlForm",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "AcademicYearId",
                table: "GradeBooks",
                newName: "SyllabusHours");

            migrationBuilder.AddColumn<int>(
                name: "ControlForm",
                table: "GradeBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditsAmount",
                table: "GradeBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
