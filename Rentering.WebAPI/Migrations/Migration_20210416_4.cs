using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_4)]
    public class Migration_20210416_4 : Migration
    {
        public override void Down()
        {
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Renters_Query_GetRenterById]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Renters_Query_GetRentersOfAccount]");	
		}

        public override void Up()
        {
            #region Query
            Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Renters_Query_GetRenterById]
							@Id INT
						AS
						BEGIN
							SELECT 
								*
							FROM 
								Renters
							WHERE 
								[Id] = @Id;
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Renters_Query_GetRentersOfAccount]
							@AccountId INT
						AS
						BEGIN
							SELECT 
								*
							FROM 
								Renters
							WHERE 
								[AccountId] = @AccountId;
						END
						GO
						");
			#endregion
		}
	}
}
