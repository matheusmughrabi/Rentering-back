using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class AddFKToParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Corporation_Participants_AccountId",
                table: "Corporation_Participants",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Corporation_Participants_Account_AccountId",
                table: "Corporation_Participants",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corporation_Participants_Account_AccountId",
                table: "Corporation_Participants");

            migrationBuilder.DropIndex(
                name: "IX_Corporation_Participants_AccountId",
                table: "Corporation_Participants");
        }
    }
}
