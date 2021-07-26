using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class AllMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContractState = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,5)", nullable: false),
                    RentDueDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ParticipantRole = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountContracts_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<DateTime>(type: "Date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,5)", nullable: false),
                    ReceiverPaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PayerPaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPayments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Corporation_MonthlyBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<DateTime>(type: "Date", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "decimal(19,5)", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation_MonthlyBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporation_MonthlyBalance_Corporation_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Corporation_Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false),
                    InvitationStatus = table.Column<int>(type: "int", nullable: false),
                    SharedPercentage = table.Column<decimal>(type: "decimal(19,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporation_Participants_Corporation_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountContracts_ContractId",
                table: "AccountContracts",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPayments_ContractId",
                table: "ContractPayments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_MonthlyBalance_CorporationId",
                table: "Corporation_MonthlyBalance",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_Participants_CorporationId",
                table: "Corporation_Participants",
                column: "CorporationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountContracts");

            migrationBuilder.DropTable(
                name: "ContractPayments");

            migrationBuilder.DropTable(
                name: "Corporation_MonthlyBalance");

            migrationBuilder.DropTable(
                name: "Corporation_Participants");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Corporation");
        }
    }
}
