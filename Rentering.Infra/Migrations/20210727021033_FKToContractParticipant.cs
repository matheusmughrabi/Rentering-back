using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class FKToContractParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountContracts_AccountId",
                table: "AccountContracts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountContracts_Account_AccountId",
                table: "AccountContracts",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountContracts_Account_AccountId",
                table: "AccountContracts");

            migrationBuilder.DropIndex(
                name: "IX_AccountContracts_AccountId",
                table: "AccountContracts");
        }
    }
}
