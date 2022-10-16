using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class GroupExpense14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseExpense_Person_CreatorId",
                table: "BaseExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseExpense_Person_CreatorId",
                table: "BaseExpense",
                column: "CreatorId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseExpense_Person_CreatorId",
                table: "BaseExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseExpense_Person_CreatorId",
                table: "BaseExpense",
                column: "CreatorId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
