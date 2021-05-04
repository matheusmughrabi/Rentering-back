using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210502_1)]
    public class Migration_20210502_1 : Migration
    {
        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_GetContractById]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_GetAllContracts]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_CheckIfContractNameExists]");

            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_CreateContract]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_UpdateContract]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_DeleteContract]");
        }

        public override void Up()
        {
			#region Query
			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_GetContractById]
							@Id INT
						AS
						BEGIN
							SELECT
								*
							FROM 
								ContractsWithGuarantor
							WHERE 
								[Id] = @Id;
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_GetAllContracts]

						AS
						BEGIN
							SELECT 
								*
							FROM 
								ContractsWithGuarantor
						END
						GO
						");

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

                        CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_CreateContract]
	                        @ContractName nvarchar(255),
							@RenterId INT,
							@TenantId INT,
							@GuarantorId INT,
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
								[RenterId],
								[TenantId],
								[GuarantorId],
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
								@RenterId,
								@TenantId,
								@GuarantorId,
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

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_UpdateContract]
							@Id INT,
							@ContractName nvarchar(255),
							@RenterId INT,
							@TenantId INT,
							@GuarantorId INT,
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
							UPDATE 
								ContractsWithGuarantor
							SET
								[ContractName] = @ContractName,
								[RenterId] = @RenterId,
								[TenantId] = @TenantId,
								[GuarantorId] = @GuarantorId,
								[Street] = @Street,
								[Neighborhood] = @Neighborhood,
								[City] = @City,
								[CEP] = @CEP,
								[State] = @State,
								[PropertyRegistrationNumber] = @PropertyRegistrationNumber,
								[RentPrice] = @RentPrice,
								[RentDueDate] = @RentDueDate,
								[ContractStartDate] = @ContractStartDate,
								[ContractEndDate] = @ContractEndDate
							WHERE 
								Id = @Id
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO



						CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_DeleteContract]
							@Id INT
						AS
						BEGIN
							DELETE FROM 
								ContractsWithGuarantor
							WHERE 
								Id = @Id
						END
						GO");
			#endregion
		}
	}
}
