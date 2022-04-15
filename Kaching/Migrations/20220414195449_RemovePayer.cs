using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class RemovePayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Payer_PayerId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense");

            migrationBuilder.DropTable(
                name: "Payer");

            migrationBuilder.DropIndex(
                name: "IX_Expense_PayerId",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "PayerId",
                table: "Expense",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_BuyerId",
                table: "Expense",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_BuyerId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_BuyerId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Expense",
                newName: "PayerId");

            migrationBuilder.CreateTable(
                name: "Payer",
                columns: table => new
                {
                    PayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payer", x => x.PayerId);
                    table.ForeignKey(
                        name: "FK_Payer_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_PayerId",
                table: "Expense",
                column: "PayerId",
                unique: true,
                filter: "[PayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payer_PersonId",
                table: "Payer",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Payer_PayerId",
                table: "Expense",
                column: "PayerId",
                principalTable: "Payer",
                principalColumn: "PayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person_PersonId",
                table: "Expense",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
