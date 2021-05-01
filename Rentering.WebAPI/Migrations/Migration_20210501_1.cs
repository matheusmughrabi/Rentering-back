using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210501_1)]
    public class Migration_20210501_1 : Migration
    {
		public override void Down()
		{
			Delete.Table("ContractsWithGuarantor");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_CreateContract]");
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

			Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_CreateContract]
	                        @ContractName nvarchar(255),
	                        @Street nvarchar(255),
	                        @Neighborhood nvarchar(255),
	                        @City nvarchar(255),
	                        @CEP nvarchar(255),
	                        @State int,
	                        @PropertyRegistrationNumber int,
	                        @RentPrice decimal(19, 5),
	                        @RentDueDate date,
	                        @ContractStartDate date,
	                        @ContractEndDate date
                        AS
                        BEGIN
                           
						INSERT INTO [ContractsWithGuarantor] (
								[ContractName], 
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[PropertyRegistrationNumber],
								[RentPrice],
								[RentDueDate],
								[ContractStartDate],
								[ContractEndDate]
							) VALUES (
								@ContractName,
								@Street,
								@Neighborhood,
								@City,
								@CEP,
								@State,
								@PropertyRegistrationNumber,
								@RentPrice,
								@RentDueDate,
								@ContractStartDate,
								@ContractEndDate
							)

                        END
                        GO");
		}
	}
}
