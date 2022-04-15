using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class AddCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Expense",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_PersonId",
                table: "Expense",
                newName: "IX_Expense_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense",
                column: "CreatorId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_CreatorId",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Expense",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_CreatorId",
                table: "Expense",
                newName: "IX_Expense_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }
    }
}
