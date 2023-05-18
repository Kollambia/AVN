using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVN.Data.Migrations
{
    public partial class AddLinkBetweenStudentPaymentsAndStudentPaymentsDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentPaymentId",
                table: "StudentPaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPaymentDetails_StudentPaymentId",
                table: "StudentPaymentDetails",
                column: "StudentPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails",
                column: "StudentPaymentId",
                principalTable: "StudentPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPaymentDetails_StudentPayments_StudentPaymentId",
                table: "StudentPaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_StudentPaymentDetails_StudentPaymentId",
                table: "StudentPaymentDetails");

            migrationBuilder.DropColumn(
                name: "StudentPaymentId",
                table: "StudentPaymentDetails");
        }
    }
}
