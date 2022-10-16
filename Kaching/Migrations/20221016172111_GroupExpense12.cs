using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations
{
    public partial class GroupExpense12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseExpense_Group_GroupId",
                table: "BaseExpense");

            migrationBuilder.DropIndex(
                name: "IX_BaseExpense_GroupId",
                table: "BaseExpense");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "BaseExpense");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "BaseExpense",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseExpense_GroupId",
                table: "BaseExpense",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseExpense_Group_GroupId",
                table: "BaseExpense",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");
        }
    }
}
