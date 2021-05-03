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

			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Renters_CUD_CreateRenter]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Renters_CUD_DeleteRenter]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Renters_CUD_UpdateRenter]");			
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
								[Id],
								[AccountId],
								[FirstName], 
								[LastName],
								[Nationality],
								[Ocupation],
								[MaritalStatus],
								[IdentityRG],
								[CPF],
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[SpouseFirstName],
								[SpouseLastName],
								[SpouseNationality],
								[SpouseIdentityRG],
								[SpouseCPF]
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
								[Id],
								[AccountId],
								[FirstName], 
								[LastName],
								[Nationality],
								[Ocupation],
								[MaritalStatus],
								[IdentityRG],
								[CPF],
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[SpouseFirstName],
								[SpouseLastName],
								[SpouseNationality],
								[SpouseIdentityRG],
								[SpouseCPF]
							FROM 
								Renters
							WHERE 
								[AccountId] = @AccountId;
						END
						GO
						");
			#endregion

			#region CUD	
            Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Renters_CUD_CreateRenter]
							@AccountId INT,
							@FirstName NVARCHAR(50),
							@LastName NVARCHAR(50),
							@Nationality NVARCHAR(50),
							@Ocupation NVARCHAR(50),
							@MaritalStatus INT,
							@IdentityRG NVARCHAR(50),
							@CPF NVARCHAR(50),
							@Street NVARCHAR(50),
							@Neighborhood NVARCHAR(50),
							@City NVARCHAR(50),
							@CEP NVARCHAR(50),
							@State INT,
							@SpouseFirstName NVARCHAR(50),
							@SpouseLastName NVARCHAR(50),
							@SpouseNationality NVARCHAR(50),
							@SpouseIdentityRG NVARCHAR(50),
							@SpouseCPF NVARCHAR(50)
						AS
						BEGIN
							INSERT INTO [Renters] (
								[AccountId],
								[FirstName], 
								[LastName],
								[Nationality],
								[Ocupation],
								[MaritalStatus],
								[IdentityRG],
								[CPF],
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[SpouseFirstName],
								[SpouseLastName],
								[SpouseNationality],
								[SpouseIdentityRG],
								[SpouseCPF]
							) VALUES (
								@AccountId,
								@FirstName,
								@LastName,
								@Nationality,
								@Ocupation,
								@MaritalStatus,
								@IdentityRG,
								@CPF,
								@Street,
								@Neighborhood,
								@City,
								@CEP,
								@State,
								@SpouseFirstName,
								@SpouseLastName,
								@SpouseNationality,
								@SpouseIdentityRG,
								@SpouseCPF
							)
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Renters_CUD_UpdateRenter]
							@Id INT,
							@AccountId INT,
							@FirstName NVARCHAR(50),
							@LastName NVARCHAR(50),
							@Nationality NVARCHAR(50),
							@Ocupation NVARCHAR(50),
							@MaritalStatus INT,
							@IdentityRG NVARCHAR(50),
							@CPF NVARCHAR(50),
							@Street NVARCHAR(50),
							@Neighborhood NVARCHAR(50),
							@City NVARCHAR(50),
							@CEP NVARCHAR(50),
							@State INT,
							@SpouseFirstName NVARCHAR(50),
							@SpouseLastName NVARCHAR(50),
							@SpouseNationality NVARCHAR(50),
							@SpouseIdentityRG NVARCHAR(50),
							@SpouseCPF NVARCHAR(50)
						AS
						BEGIN
							UPDATE 
								Renters
							SET
								[AccountId] = @AccountId,
								[FirstName] = @FirstName, 
								[LastName] = @LastName,
								[Nationality] = @Nationality,
								[Ocupation] = @Ocupation,
								[MaritalStatus] = @MaritalStatus,
								[IdentityRG] = @IdentityRG,
								[CPF] = @CPF,
								[Street] = @Street,
								[Neighborhood] = @Neighborhood,
								[City] = @City,
								[CEP] = @CEP,
								[State] = @State,
								[SpouseFirstName] = @SpouseFirstName,
								[SpouseLastName] = @SpouseLastName,
								[SpouseNationality] = @SpouseNationality,
								[SpouseIdentityRG] = @SpouseIdentityRG,
								[SpouseCPF] = @SpouseCPF
							WHERE 
								Id = @Id
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO



						CREATE PROCEDURE [dbo].[sp_Renters_CUD_DeleteRenter]
							@Id INT
						AS
						BEGIN
							DELETE FROM 
								Renters
							WHERE 
								Id = @Id
						END
						GO");
			#endregion
		}
	}
}
