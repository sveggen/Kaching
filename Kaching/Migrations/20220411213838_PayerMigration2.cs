using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class PayerMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Expense");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Expense",
                type: "int",
                nullable: true);
        }
    }
}
