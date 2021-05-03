using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210502_1)]
    public class Migration_20210502_1 : Migration
    {
        public override void Down()
        {
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_CheckIfContractNameExists]");

			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_CreateContract]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_InviteRenter]");
		}

        public override void Up()
        {
            #region Query
            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_CheckIfContractNameExists]
	                        @ContractName NVARCHAR(255)
                        AS
	                        SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [ContractsWithGuarantor]
		                        WHERE [ContractName] = @ContractName
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT) END
                        GO");
			#endregion

			#region CUD
			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_InviteRenter]
							@Id INT,
							@RenterId INT
						AS
						BEGIN
							UPDATE 
								ContractsWithGuarantor
							SET
								[RenterId] = @RenterId
							WHERE 
								Id = @Id
						END
						GO");

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
			#endregion
		}
	}
}
