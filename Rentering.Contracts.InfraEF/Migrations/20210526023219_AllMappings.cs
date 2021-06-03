using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Contracts.InfraEF.Migrations
{
    public partial class AllMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstateContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,5)", nullable: false),
                    RentDueDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateContracts", x => x.Id);
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
                        name: "FK_AccountContracts_EstateContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EstateContracts",
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
                    RenterPaymentStatus = table.Column<int>(type: "int", nullable: false),
                    TenantPaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPayments_EstateContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EstateContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guarantors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    GuarantorStatus = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseOcupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guarantors_EstateContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EstateContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    RenterStatus = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renters_EstateContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EstateContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    TenantStatus = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseOcupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_EstateContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EstateContracts",
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
                name: "IX_Guarantors_ContractId",
                table: "Guarantors",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_ContractId",
                table: "Renters",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ContractId",
                table: "Tenants",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountContracts");

            migrationBuilder.DropTable(
                name: "ContractPayments");

            migrationBuilder.DropTable(
                name: "Guarantors");

            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "EstateContracts");
        }
    }
}
