using FluentMigrator;
using System;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_5)]
    public class Migration_20210416_5 : Migration
    {
        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Util_CheckIfEmailExists]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Util_CheckIfUsernameExists]");
        }

        public override void Up()
        {
            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Accounts_Util_CheckIfEmailExists]
	                        @Email NVARCHAR(50)
                        AS
	                        SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Email] = @Email
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT) END
                        GO");

            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Accounts_Util_CheckIfUsernameExists]
	                        @Username NVARCHAR(50)
                        AS
	                        SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Username] = @Username
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT) END
                        GO");
        }
    }
}
