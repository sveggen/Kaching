using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations.Data
{
    public partial class RefactoredMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Category");
        }
    }
}
