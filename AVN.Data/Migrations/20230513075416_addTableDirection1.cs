using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class addTableDirection1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direction_Departments_DepartmentId",
                table: "Direction");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Direction_DirectionId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direction",
                table: "Direction");

            migrationBuilder.RenameTable(
                name: "Direction",
                newName: "Directions");

            migrationBuilder.RenameIndex(
                name: "IX_Direction_DepartmentId",
                table: "Directions",
                newName: "IX_Directions_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directions",
                table: "Directions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directions",
                table: "Directions");

            migrationBuilder.RenameTable(
                name: "Directions",
                newName: "Direction");

            migrationBuilder.RenameIndex(
                name: "IX_Directions_DepartmentId",
                table: "Direction",
                newName: "IX_Direction_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direction",
                table: "Direction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direction_Departments_DepartmentId",
                table: "Direction",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Direction_DirectionId",
                table: "Groups",
                column: "DirectionId",
                principalTable: "Direction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
