﻿using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210501_1)]
    public class Migration_20210501_1 : Migration
    {
		public override void Down()
		{
			Delete.Table("ContractsWithGuarantor");
			
		}

		public override void Up()
		{
			Create.Table("ContractsWithGuarantor")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
				.WithColumn("ContractName").AsString().NotNullable()
				.WithColumn("RenterId").AsInt32().Nullable()
				.WithColumn("TenantId").AsInt32().Nullable()
				.WithColumn("GuarantorId").AsInt32().Nullable()
				.WithColumn("Street").AsString().NotNullable()
				.WithColumn("Neighborhood").AsString().NotNullable()
				.WithColumn("City").AsString().NotNullable()
				.WithColumn("CEP").AsString().NotNullable()
				.WithColumn("State").AsInt32().NotNullable()
				.WithColumn("PropertyRegistrationNumber").AsInt32().NotNullable()
				.WithColumn("RentPrice").AsDecimal().NotNullable()
				.WithColumn("RentDueDate").AsDate().NotNullable()
				.WithColumn("ContractStartDate").AsDate().NotNullable()
				.WithColumn("ContractEndDate").AsDate().NotNullable();
		}
	}
}
