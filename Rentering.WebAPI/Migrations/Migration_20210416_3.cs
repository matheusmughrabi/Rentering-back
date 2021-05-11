using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_3)]
    public class Migration_20210416_3 : Migration
    {
        public override void Down()
        {
            //Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Query_GetAccountById]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Query_GetAllAccounts]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Query_CheckIfEmailExists]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Query_CheckIfUsernameExists]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_Query_CheckIfAccountExists]");

            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_CUD_CreateAccount]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_CUD_DeleteAccount]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Accounts_CUD_UpdateAccount]");
        }

        public override void Up()
        {
            #region QueryRepository
            //Execute.Sql(@"SET ANSI_NULLS ON
            //                GO
            //                SET QUOTED_IDENTIFIER ON
            //                GO

            //                CREATE PROCEDURE sp_Accounts_Query_GetAccountById
            //                    @Id INT
            //                AS
            //                BEGIN
	           //                 SET NOCOUNT ON;

	           //                 SELECT * FROM Accounts WHERE Id = @Id
            //                END
            //                GO");

            Execute.Sql(@"SET ANSI_NULLS ON
                            GO
                            SET QUOTED_IDENTIFIER ON
                            GO

                            CREATE PROCEDURE sp_Accounts_Query_GetAllAccounts
                            AS
                            BEGIN
	                            SET NOCOUNT ON;

	                            SELECT * FROM Accounts
                            END
                            GO");

            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Accounts_Query_CheckIfEmailExists]
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

                        CREATE PROCEDURE [dbo].[sp_Accounts_Query_CheckIfUsernameExists]
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

            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO


                        CREATE PROCEDURE [dbo].[sp_Accounts_Query_CheckIfAccountExists]
	                        @Id int
                        AS
	                        SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Id] = @Id
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT) END
                        GO");
            #endregion

            # region CUDRepository
            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO


                        CREATE PROCEDURE [dbo].[sp_Accounts_CUD_CreateAccount]
                            @Email NVARCHAR(50),
                            @Username NVARCHAR(50),
                            @Password NVARCHAR(50),
	                        @Role int
                        AS
                        BEGIN
                            INSERT INTO [Accounts] (
                                [Email], 
                                [Username], 
                                [Password],
		                        [Role]
                            ) VALUES (
                                @Email,
                                @Username,
                                @Password,
		                        @Role
                            )
                        END
                        GO");

            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Accounts_CUD_DeleteAccount]
	                        @Id INT
                        AS
                        BEGIN
	                        DELETE FROM 
		                        Accounts
	                        WHERE 
		                        Id = @Id
                        END
                        GO");

            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Accounts_CUD_UpdateAccount]
	                        @Id INT,
	                        @Email NVARCHAR(50),
	                        @Username NVARCHAR(50),
	                        @Password NVARCHAR(50),
	                        @Role int
                        AS
                        BEGIN
	                        UPDATE 
		                        Accounts
	                        SET 
		                        Email = @Email,
		                        Username = @Username,
		                        Password = @Password,
		                        Role = @Role
	                        WHERE 
		                        Id = @Id
                        END
                        GO");
            #endregion
        }
    }
}
