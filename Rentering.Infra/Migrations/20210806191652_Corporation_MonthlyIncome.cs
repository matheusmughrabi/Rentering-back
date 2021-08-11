using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class Corporation_MonthlyIncome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corporation_Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,5)", nullable: false),
                    MonthlyBalanceId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporation_Incomes_Corporation_MonthlyBalance_MonthlyBalanceId",
                        column: x => x.MonthlyBalanceId,
                        principalTable: "Corporation_MonthlyBalance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_Incomes_MonthlyBalanceId",
                table: "Corporation_Incomes",
                column: "MonthlyBalanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Corporation_Incomes");
        }
    }
}
