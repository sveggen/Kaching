using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class PayerMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expense_PayerId",
                table: "Expense");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_PayerId",
                table: "Expense",
                column: "PayerId",
                unique: true,
                filter: "[PayerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expense_PayerId",
                table: "Expense");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_PayerId",
                table: "Expense",
                column: "PayerId");
        }
    }
}
