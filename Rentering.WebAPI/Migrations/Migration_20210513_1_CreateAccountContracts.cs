using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210513_1, "CreateAccountContracts")]
    public class Migration_20210513_1_CreateAccountContracts : Migration
    {
        public override void Down()
        {
            Delete.Table("AccountContracts");
        }

        public override void Up()
        {
            Create.Table("AccountContracts")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("AccountId").AsInt32().NotNullable().ForeignKey("Accounts", "Id")
                .WithColumn("ContractId").AsInt32().NotNullable().ForeignKey("EstateContracts", "Id")
                .WithColumn("ParticipantRole").AsInt32().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable();
        }
    }
}
