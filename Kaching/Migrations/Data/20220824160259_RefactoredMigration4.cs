using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations.Data
{
    public partial class RefactoredMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Personal",
                table: "Group",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Personal",
                table: "Group");
        }
    }
}
