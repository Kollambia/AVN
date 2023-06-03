using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class reworkDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AcademicDegree",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "StudingForm",
                table: "Students",
                newName: "RecruitmentYear");

            migrationBuilder.RenameColumn(
                name: "AcademicYear",
                table: "StudentPayments",
                newName: "RecruitmentYear");

            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Orders",
                newName: "OrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StudentId",
                table: "Orders",
                newName: "IX_Orders_StudentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsHasDebt",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "StudentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcademicDegree",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudingForm",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingPeriod",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AcademicDegree",
                table: "Directions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingPeriod",
                table: "Directions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovementTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearFrom = table.Column<int>(type: "int", nullable: false),
                    YearTo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTypes_MovementTypes_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldGroupId = table.Column<int>(type: "int", nullable: false),
                    NewGroupId = table.Column<int>(type: "int", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementTypeId = table.Column<int>(type: "int", nullable: true),
                    AcademicYearId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    StudentId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMovements_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentMovements_MovementTypes_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentMovements_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_AcademicYearId",
                table: "StudentPayments",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_GroupId",
                table: "StudentPayments",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AcademicYearId",
                table: "Groups",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AcademicYearId",
                table: "Orders",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GroupId",
                table: "Orders",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MovementTypeId",
                table: "Orders",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTypes_MovementTypeId",
                table: "OrderTypes",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMovements_AcademicYearId",
                table: "StudentMovements",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMovements_MovementTypeId",
                table: "StudentMovements",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMovements_StudentId1",
                table: "StudentMovements",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AcademicYears_AcademicYearId",
                table: "Groups",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AcademicYears_AcademicYearId",
                table: "Orders",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Groups_GroupId",
                table: "Orders",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MovementTypes_MovementTypeId",
                table: "Orders",
                column: "MovementTypeId",
                principalTable: "MovementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_AcademicYears_AcademicYearId",
                table: "StudentPayments",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Groups_GroupId",
                table: "StudentPayments",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AcademicYears_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AcademicYears_AcademicYearId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Groups_GroupId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MovementTypes_MovementTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_AcademicYears_AcademicYearId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Groups_GroupId",
                table: "StudentPayments");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "StudentMovements");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "MovementTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentPayments_AcademicYearId",
                table: "StudentPayments");

            migrationBuilder.DropIndex(
                name: "IX_StudentPayments_GroupId",
                table: "StudentPayments");

            migrationBuilder.DropIndex(
                name: "IX_Groups_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AcademicYearId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GroupId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MovementTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsHasDebt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "AcademicDegree",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StudingForm",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TrainingPeriod",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AcademicDegree",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "TrainingPeriod",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MovementTypeId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "RecruitmentYear",
                table: "Students",
                newName: "StudingForm");

            migrationBuilder.RenameColumn(
                name: "RecruitmentYear",
                table: "StudentPayments",
                newName: "AcademicYear");

            migrationBuilder.RenameColumn(
                name: "OrderTypeId",
                table: "Order",
                newName: "OrderType");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StudentId",
                table: "Order",
                newName: "IX_Order_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "AcademicDegree",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Students_StudentId",
                table: "Order",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
