using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class fixbugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subjects_SubjectId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Schedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectId",
                table: "Schedules",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subjects_SubjectId",
                table: "Schedules",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subjects_SubjectId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectId",
                table: "Schedules");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId1",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectId1",
                table: "Schedules",
                column: "SubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subjects_SubjectId1",
                table: "Schedules",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
