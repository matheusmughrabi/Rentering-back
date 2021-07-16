using Microsoft.EntityFrameworkCore.Migrations;

namespace Rentering.Infra.Migrations
{
    public partial class SimplificationsInDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountContracts_Account_AccountId",
                table: "AccountContracts");

            migrationBuilder.DropTable(
                name: "Guarantors");

            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_AccountContracts_AccountId",
                table: "AccountContracts");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "EstateContracts");

            migrationBuilder.DropColumn(
                name: "City",
                table: "EstateContracts");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "EstateContracts");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "EstateContracts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "EstateContracts");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "EstateContracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "EstateContracts",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "EstateContracts",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "EstateContracts",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "EstateContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "EstateContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "EstateContracts",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Guarantors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    GuarantorStatus = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseOcupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true)
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
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RenterStatus = table.Column<int>(type: "int", nullable: false),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true)
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
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ocupation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseNationality = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseOcupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TenantStatus = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseCPF = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseIdentityRG = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SpouseFirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", nullable: true)
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
                name: "IX_AccountContracts_AccountId",
                table: "AccountContracts",
                column: "AccountId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AccountContracts_Account_AccountId",
                table: "AccountContracts",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
