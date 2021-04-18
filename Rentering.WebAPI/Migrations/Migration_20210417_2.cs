using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210417_2)]
    public class Migration_20210417_2 : Migration
    {
        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_Auth_GetTenantsIdsOfAccount]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_CUD_CreateTenant]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_CUD_DeleteTenant]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_CUD_UpdateTenant]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_Query_GetTenantById]");
            Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Tenants_Query_GetTenantsOfAccount]");
        }

        public override void Up()
        {
            Execute.Sql(@"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE PROCEDURE [dbo].[sp_Tenants_Auth_GetTenantsIdsOfAccount]
	                        @AccountId int
                        AS
                        BEGIN
                            SELECT [Id]
	                        FROM Tenants
	                        WHERE [AccountId] = @AccountId
                        END
                        GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Tenants_CUD_CreateTenant]
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
							@SpouseOcupation NVARCHAR(50),
							@SpouseIdentityRG NVARCHAR(50),
							@SpouseCPF NVARCHAR(50)
						AS
						BEGIN
							INSERT INTO [Tenants] (
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
								[SpouseOcupation],
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
								@SpouseOcupation,
								@SpouseIdentityRG,
								@SpouseCPF
							)
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO



						CREATE PROCEDURE [dbo].[sp_Tenants_CUD_DeleteTenant]
							@Id INT
						AS
						BEGIN
							DELETE FROM 
								Tenants
							WHERE 
								Id = @Id
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Tenants_CUD_UpdateTenant]
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
							@SpouseOcupation NVARCHAR(50),
							@SpouseIdentityRG NVARCHAR(50),
							@SpouseCPF NVARCHAR(50)
						AS
						BEGIN
							UPDATE 
								Tenants
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
								[SpouseOcupation] = @SpouseOcupation,
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

						CREATE PROCEDURE [dbo].[sp_Tenants_Query_GetTenantById]
							@Id INT
						AS
						BEGIN
							SELECT 
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
								[SpouseOcupation],
								[SpouseIdentityRG],
								[SpouseCPF]
							FROM 
								Tenants
							WHERE 
								[Id] = @Id;
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Tenants_Query_GetTenantsOfAccount]
							@AccountId INT
						AS
						BEGIN
							SELECT 
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
								[SpouseOcupation],
								[SpouseIdentityRG],
								[SpouseCPF]
							FROM 
								Tenants
							WHERE 
								[AccountId] = @AccountId;
						END
						GO
						");
		}
    }
}
