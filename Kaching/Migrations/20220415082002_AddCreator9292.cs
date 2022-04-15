using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class AddCreator9292 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense",
                column: "CreatorId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense",
                column: "CreatorId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }
    }
}
