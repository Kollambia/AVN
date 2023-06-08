using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class newSubjectAndEmployeeLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_EmployeeId",
                table: "Subjects",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Employees_EmployeeId",
                table: "Subjects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Employees_EmployeeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_EmployeeId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Subjects");
        }
    }
}
