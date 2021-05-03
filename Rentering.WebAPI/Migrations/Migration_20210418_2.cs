using FluentMigrator;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210418_2)]
    public class Migration_20210418_2 : Migration
    {
		public override void Down()
		{
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Guarantors_Query_GetGuarantorById]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Guarantors_Query_GetGuarantorsOfAccount]");

			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Guarantors_CUD_CreateGuarantor]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Guarantors_CUD_UpdateGuarantor]");
			Execute.Sql(@"DROP PROCEDURE [dbo].[sp_Guarantors_CUD_DeleteGuarantor]");
		}

		public override void Up()
		{
			#region Query
			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Guarantors_Query_GetGuarantorById]
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
								[SpouseOcupation],
								[SpouseIdentityRG],
								[SpouseCPF]
							FROM 
								Guarantors
							WHERE 
								[Id] = @Id;
						END
						GO");

			Execute.Sql(@"SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE PROCEDURE [dbo].[sp_Guarantors_Query_GetGuarantorsOfAccount]
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
								[SpouseOcupation],
								[SpouseIdentityRG],
								[SpouseCPF]
							FROM 
								Guarantors
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

						CREATE PROCEDURE [dbo].[sp_Guarantors_CUD_CreateGuarantor]
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
							INSERT INTO [Guarantors] (
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

						CREATE PROCEDURE [dbo].[sp_Guarantors_CUD_UpdateGuarantor]
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
								Guarantors
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



						CREATE PROCEDURE [dbo].[sp_Guarantors_CUD_DeleteGuarantor]
							@Id INT
						AS
						BEGIN
							DELETE FROM 
								Guarantors
							WHERE 
								Id = @Id
						END
						GO");
			#endregion
		}
	}
}
