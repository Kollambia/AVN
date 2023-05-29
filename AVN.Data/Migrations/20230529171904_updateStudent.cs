using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class updateStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "SName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SName",
                table: "Students",
                newName: "FullName");
        }
    }
}
