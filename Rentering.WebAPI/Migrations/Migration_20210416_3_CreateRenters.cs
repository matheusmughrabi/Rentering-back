using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_3, "CreateRenters")]
    public class Migration_20210416_3_CreateRenters : Migration
    {
        public override void Down()
        {
            Delete.Table("Renters");
        }

        public override void Up()
        {
            Create.Table("Renters")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("ContractId").AsInt32().NotNullable().ForeignKey("EstateContracts", "Id")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Nationality").AsString().NotNullable()
                .WithColumn("Ocupation").AsString().NotNullable()
                .WithColumn("MaritalStatus").AsInt32().NotNullable()
                .WithColumn("IdentityRG").AsString().NotNullable()
                .WithColumn("CPF").AsString().NotNullable()
                .WithColumn("Street").AsString().NotNullable()
                .WithColumn("Neighborhood").AsString().NotNullable()
                .WithColumn("City").AsString().NotNullable()
                .WithColumn("CEP").AsString().NotNullable()
                .WithColumn("State").AsInt32().NotNullable()
                .WithColumn("SpouseFirstName").AsString().Nullable()
                .WithColumn("SpouseLastName").AsString().Nullable()
                .WithColumn("SpouseNationality").AsString().Nullable()
                .WithColumn("SpouseIdentityRG").AsString().Nullable()
                .WithColumn("SpouseCPF").AsString().Nullable();
        }
    }
}
