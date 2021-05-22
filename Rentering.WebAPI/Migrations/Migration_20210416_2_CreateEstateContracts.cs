using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_2, "CreateEstateContracts")]
    public class Migration_20210416_2_CreateEstateContracts : Migration
    {
		public override void Down()
		{
			Delete.Table("EstateContracts");
			
		}

		public override void Up()
		{
			Create.Table("EstateContracts")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
				.WithColumn("ContractName").AsString(100).NotNullable().Unique()
				.WithColumn("Street").AsString(100).NotNullable()
				.WithColumn("Neighborhood").AsString(100).NotNullable()
				.WithColumn("City").AsString(100).NotNullable()
				.WithColumn("CEP").AsString(100).NotNullable()
				.WithColumn("State").AsInt32().NotNullable()
				.WithColumn("PropertyRegistrationNumber").AsInt32().NotNullable()
				.WithColumn("RentPrice").AsDecimal().NotNullable()
				.WithColumn("RentDueDate").AsDate().NotNullable()
				.WithColumn("ContractStartDate").AsDate().NotNullable()
				.WithColumn("ContractEndDate").AsDate().NotNullable();
		}
	}
}
