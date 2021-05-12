using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210417_1, "CreateTenants")]
    public class Migration_20210417_1_CreateTenants : Migration
    {
        public override void Down()
        {
            Delete.Table("Tenants");
        }

        public override void Up()
        {
            Create.Table("Tenants")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("AccountId").AsInt32().NotNullable().ForeignKey("Accounts", "Id")
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
                .WithColumn("SpouseOcupation").AsString().Nullable()
                .WithColumn("SpouseIdentityRG").AsString().Nullable()
                .WithColumn("SpouseCPF").AsString().Nullable();
        }
    }
}
