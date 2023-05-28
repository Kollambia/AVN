using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEmployees_Employees_EmployeeId",
                table: "SubjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEmployees_Subjects_SubjectId",
                table: "SubjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "SubjectEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SubjectEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StudentPaymentId",
                table: "StudentPaymentDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DirectionId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DirectionName",
                table: "Directions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Directions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails",
                column: "StudentPaymentId",
                principalTable: "StudentPayments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEmployees_Employees_EmployeeId",
                table: "SubjectEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEmployees_Subjects_SubjectId",
                table: "SubjectEmployees",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEmployees_Employees_EmployeeId",
                table: "SubjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEmployees_Subjects_SubjectId",
                table: "SubjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "SubjectEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SubjectEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentPayments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentPaymentId",
                table: "StudentPaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DirectionId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DirectionName",
                table: "Directions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Directions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Departments_DepartmentId",
                table: "Directions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails",
                column: "StudentPaymentId",
                principalTable: "StudentPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEmployees_Employees_EmployeeId",
                table: "SubjectEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEmployees_Subjects_SubjectId",
                table: "SubjectEmployees",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
