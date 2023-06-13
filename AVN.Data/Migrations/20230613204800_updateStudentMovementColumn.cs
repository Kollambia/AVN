using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class updateStudentMovementColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentMovements_Students_StudentId1",
                table: "StudentMovements");

            migrationBuilder.DropIndex(
                name: "IX_StudentMovements_StudentId1",
                table: "StudentMovements");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentMovements");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentMovements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMovements_StudentId",
                table: "StudentMovements",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMovements_Students_StudentId",
                table: "StudentMovements",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentMovements_Students_StudentId",
                table: "StudentMovements");

            migrationBuilder.DropIndex(
                name: "IX_StudentMovements_StudentId",
                table: "StudentMovements");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentMovements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId1",
                table: "StudentMovements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMovements_StudentId1",
                table: "StudentMovements",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMovements_Students_StudentId1",
                table: "StudentMovements",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
