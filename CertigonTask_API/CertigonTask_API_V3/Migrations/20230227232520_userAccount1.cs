using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertigonTask_API_V3.Migrations
{
    public partial class userAccount1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_UserAccount_KorisnickiNalogId",
                table: "AutentifikacijaToken");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AutentifikacijaToken",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KorisnickiNalogId",
                table: "AutentifikacijaToken",
                newName: "UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AutentifikacijaToken_KorisnickiNalogId",
                table: "AutentifikacijaToken",
                newName: "IX_AutentifikacijaToken_UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_UserAccount_UserAccountId",
                table: "AutentifikacijaToken",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_UserAccount_UserAccountId",
                table: "AutentifikacijaToken");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AutentifikacijaToken",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "AutentifikacijaToken",
                newName: "KorisnickiNalogId");

            migrationBuilder.RenameIndex(
                name: "IX_AutentifikacijaToken_UserAccountId",
                table: "AutentifikacijaToken",
                newName: "IX_AutentifikacijaToken_KorisnickiNalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_UserAccount_KorisnickiNalogId",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
