using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_1, "CreateAccounts")]
    public class Migration_20210416_1_CreateAccounts : Migration
    {
        public override void Down()
        {
            Delete.Table("Accounts");
        }

        public override void Up()
        {
            Create.Table("Accounts")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Role").AsInt32().NotNullable();
        }
    }
}
