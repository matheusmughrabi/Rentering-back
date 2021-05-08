using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210507_2)]
    public class Migration_20210507_2 : Migration
    {
        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_Query_GetPaymentById]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_Query_GetAllPayments]");
            //Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_Query_CheckIfContractNameExists]");

            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_CUD_CreatePayment]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_CUD_UpdatePayment]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractPayments_CUD_DeletePayment]");
        }

        public override void Up()
        {
			#region Query
			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractPayments_Query_GetPaymentById]
							@Id INT
						AS
						BEGIN
							SELECT
								*
							FROM 
								ContractPayments
							WHERE 
								[Id] = @Id;
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractPayments_Query_GetAllPayments]

						AS
						BEGIN
							SELECT 
								*
							FROM 
								ContractPayments
						END
						GO
						");

			//Execute.Sql(@"SET ANSI_NULLS ON
   //                     GO

   //                     SET QUOTED_IDENTIFIER ON
   //                     GO

   //                     CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_Query_CheckIfContractNameExists]
	  //                      @ContractName NVARCHAR(255)
   //                     AS
	  //                      SELECT CASE WHEN EXISTS (
		 //                       SELECT [Id]
		 //                       FROM [ContractsWithGuarantor]
		 //                       WHERE [ContractName] = @ContractName
	  //                      )
	  //                      THEN CAST(1 AS BIT)
	  //                      ELSE CAST(0 AS BIT) END
   //                     GO");
			#endregion

			#region CUD
			Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_ContractPayments_CUD_CreatePayment]
	                        @ContractId nvarchar(255),
	                        @Month date,
							@RentPrice decimal(19, 5),
							@RenterPaymentStatus INT,
							@TenantPaymentStatus INT
                        AS
                        BEGIN
                           
						INSERT INTO [ContractPayments] (
								[ContractId], 
								[Month],
								[RentPrice],
								[RenterPaymentStatus],
								[TenantPaymentStatus]
							) VALUES (
								@ContractId,
								@Month,
								@RentPrice,
								@RenterPaymentStatus,
								@TenantPaymentStatus
							)

                        END
                        GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractPayments_CUD_UpdateContract]
							@Id INT,
							@ContractId nvarchar(255),
	                        @Month date,
							@RentPrice decimal(19, 5),
							@RenterPaymentStatus INT,
							@TenantPaymentStatus INT
						AS
						BEGIN
							UPDATE 
								ContractPayments
							SET
								[ContractId] = @ContractId,
								[Month] = @Month,
								[RentPrice] = @RentPrice,
								[RenterPaymentStatus] = @RenterPaymentStatus,
								[TenantPaymentStatus] = @TenantPaymentStatus
							WHERE 
								Id = @Id
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_ContractPayments_CUD_DeletePayment]
							@Id INT
						AS
						BEGIN
							DELETE FROM 
								ContractPayments
							WHERE 
								Id = @Id
						END
						GO");
			#endregion
		}
	}
}
