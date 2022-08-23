using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaching.Migrations.Data
{
    public partial class RefactoredMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Group_GroupId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_GroupId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "GroupPerson",
                columns: table => new
                {
                    GroupsGroupId = table.Column<int>(type: "int", nullable: false),
                    MembersPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPerson", x => new { x.GroupsGroupId, x.MembersPersonId });
                    table.ForeignKey(
                        name: "FK_GroupPerson_Group_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPerson_Person_MembersPersonId",
                        column: x => x.MembersPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPerson_MembersPersonId",
                table: "GroupPerson",
                column: "MembersPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPerson");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_GroupId",
                table: "Person",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Group_GroupId",
                table: "Person",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");
        }
    }
}
