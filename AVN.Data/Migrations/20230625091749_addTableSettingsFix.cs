using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class addTableSettingsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Settings");
        }
    }
}
