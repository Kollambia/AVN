using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class addTableDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Departments_DepartmentId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Groups",
                newName: "DirectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DepartmentId",
                table: "Groups",
                newName: "IX_Groups_DirectionId");

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectionShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direction_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direction_DepartmentId",
                table: "Direction",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Direction_DirectionId",
                table: "Groups",
                column: "DirectionId",
                principalTable: "Direction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Direction_DirectionId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.RenameColumn(
                name: "DirectionId",
                table: "Groups",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DirectionId",
                table: "Groups",
                newName: "IX_Groups_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Departments_DepartmentId",
                table: "Groups",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
