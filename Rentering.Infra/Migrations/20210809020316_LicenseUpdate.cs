using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class LicenseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "License",
                table: "Account",
                newName: "LicenseCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicenseCode",
                table: "Account",
                newName: "License");
        }
    }
}
