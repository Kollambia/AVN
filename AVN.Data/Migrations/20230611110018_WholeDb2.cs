using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class WholeDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEmployee_Employees_EmployeeId1",
                table: "GroupEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupEmployee_Groups_GroupId1",
                table: "GroupEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupEmployee",
                table: "GroupEmployee");

            migrationBuilder.RenameTable(
                name: "GroupEmployee",
                newName: "GroupEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEmployee_GroupId1",
                table: "GroupEmployees",
                newName: "IX_GroupEmployees_GroupId1");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEmployee_EmployeeId1",
                table: "GroupEmployees",
                newName: "IX_GroupEmployees_EmployeeId1");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId1",
                table: "GroupEmployees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupEmployees",
                table: "GroupEmployees",
                columns: new[] { "GroupId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEmployees_Employees_EmployeeId1",
                table: "GroupEmployees",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEmployees_Groups_GroupId1",
                table: "GroupEmployees",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEmployees_Employees_EmployeeId1",
                table: "GroupEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupEmployees_Groups_GroupId1",
                table: "GroupEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupEmployees",
                table: "GroupEmployees");

            migrationBuilder.RenameTable(
                name: "GroupEmployees",
                newName: "GroupEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEmployees_GroupId1",
                table: "GroupEmployee",
                newName: "IX_GroupEmployee_GroupId1");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEmployees_EmployeeId1",
                table: "GroupEmployee",
                newName: "IX_GroupEmployee_EmployeeId1");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId1",
                table: "GroupEmployee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupEmployee",
                table: "GroupEmployee",
                columns: new[] { "GroupId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEmployee_Employees_EmployeeId1",
                table: "GroupEmployee",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEmployee_Groups_GroupId1",
                table: "GroupEmployee",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
