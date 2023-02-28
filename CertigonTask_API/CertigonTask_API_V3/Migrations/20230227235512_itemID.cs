using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertigonTask_API_V3.Migrations
{
    public partial class itemID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Item",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Item",
                newName: "ID");
        }
    }
}
