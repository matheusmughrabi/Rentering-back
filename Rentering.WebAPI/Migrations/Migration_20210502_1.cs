using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210502_1)]
    public class Migration_20210502_1 : Migration
    {
        public override void Down()
        {
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_InviteRenter]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_GetContractById]");
		}

        public override void Up()
        {
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

                        CREATE PROCEDURE [dbo].[sp_ContractsWithGuarantor_CUD_GetContractById]
	                        @Id INT
                        AS
                        BEGIN
                            SELECT *
	                        FROM ContractsWithGuarantor
	                        WHERE [Id] = @Id
                        END
                        GO");
		}
    }
}
