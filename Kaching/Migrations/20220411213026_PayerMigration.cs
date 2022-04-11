using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class PayerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "Expense",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payer",
                columns: table => new
                {
                    PayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
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
                column: "PayerId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Payer_PayerId",
                table: "Expense");

            migrationBuilder.DropTable(
                name: "Payer");

            migrationBuilder.DropIndex(
                name: "IX_Expense_PayerId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "Expense");
        }
    }
}
