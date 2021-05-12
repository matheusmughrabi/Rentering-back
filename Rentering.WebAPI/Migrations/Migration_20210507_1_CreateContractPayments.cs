using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210507_1, "CreateContractPayments")]
    public class Migration_20210507_1_CreateContractPayments : Migration
    {
        public override void Down()
        {
            Delete.Table("ContractPayments");
        }

        public override void Up()
        {
            Create.Table("ContractPayments")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("ContractId").AsInt32().NotNullable().ForeignKey("EstateContracts", "Id")
                .WithColumn("Month").AsDate().NotNullable()
                .WithColumn("RentPrice").AsDecimal().NotNullable()
                .WithColumn("RenterPaymentStatus").AsInt32().NotNullable()
                .WithColumn("TenantPaymentStatus").AsInt32().NotNullable();
        }
    }
}
