using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class StartEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Corporation_MonthlyBalance");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Corporation_MonthlyBalance",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Corporation_MonthlyBalance",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Corporation_MonthlyBalance");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Corporation_MonthlyBalance");

            migrationBuilder.AddColumn<DateTime>(
                name: "Month",
                table: "Corporation_MonthlyBalance",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
