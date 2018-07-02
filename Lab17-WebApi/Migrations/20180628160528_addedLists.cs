using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab17WebApi.Migrations
{
    public partial class addedLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "ToDos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToDoListID",
                table: "ToDos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ToDoListID",
                table: "ToDos",
                column: "ToDoListID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_ToDoLists_ToDoListID",
                table: "ToDos",
                column: "ToDoListID",
                principalTable: "ToDoLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_ToDoLists_ToDoListID",
                table: "ToDos");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ToDoListID",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ToDoListID",
                table: "ToDos");
        }
    }
}
