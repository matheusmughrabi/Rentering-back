using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Corporation_ParticipantBalance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Corporation_MonthlyBalance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Corporation_ParticipantBalance");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Corporation_MonthlyBalance");
        }
    }
}
