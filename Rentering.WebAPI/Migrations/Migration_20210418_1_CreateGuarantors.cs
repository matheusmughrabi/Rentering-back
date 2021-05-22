using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210418_1, "CreateGuarantors")]
    public class Migration_20210418_1_CreateGuarantors : Migration
    {
        public override void Down()
        {
            Delete.Table("Guarantors");
        }

        public override void Up()
        {
            Create.Table("Guarantors")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("ContractId").AsInt32().NotNullable().ForeignKey("EstateContracts", "Id")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("Nationality").AsString(100).NotNullable()
                .WithColumn("Ocupation").AsString(100).NotNullable()
                .WithColumn("MaritalStatus").AsInt32().NotNullable()
                .WithColumn("IdentityRG").AsString(100).NotNullable()
                .WithColumn("CPF").AsString(100).NotNullable()
                .WithColumn("Street").AsString(100).NotNullable()
                .WithColumn("Neighborhood").AsString(100).NotNullable()
                .WithColumn("City").AsString(100).NotNullable()
                .WithColumn("CEP").AsString(100).NotNullable()
                .WithColumn("State").AsInt32().NotNullable()
                .WithColumn("SpouseFirstName").AsString(100).Nullable()
                .WithColumn("SpouseLastName").AsString(100).Nullable()
                .WithColumn("SpouseNationality").AsString(100).Nullable()
                .WithColumn("SpouseOcupation").AsString(100).Nullable()
                .WithColumn("SpouseIdentityRG").AsString(100).Nullable()
                .WithColumn("SpouseCPF").AsString(100).Nullable();
        }
    }
}
