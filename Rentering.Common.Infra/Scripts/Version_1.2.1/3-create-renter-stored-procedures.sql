/****** Object:  StoredProcedure [dbo].[spCheckIfAccountExists]    Script Date: 13/04/2021 11:47:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Accounts_Util_CheckIfAccountExists]
	@Id int
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [RenteringUsers]
		WHERE [Id] = @Id
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO


/****** Object:  StoredProcedure [dbo].[sp_Renter_CUD_CreateAccount]    Script Date: 13/04/2021 12:10:32 ******/
SET ANSI_NULLS ON
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
	@Bairro NVARCHAR(50),
	@Cidade NVARCHAR(50),
	@CEP NVARCHAR(50),
	@Estado NVARCHAR(50),
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
		[Bairro],
		[Cidade],
		[CEP],
		[Estado],
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
		@Bairro,
		@Cidade,
		@CEP,
		@Estado,
		@SpouseFirstName,
		@SpouseLastName,
		@SpouseNationality,
		@SpouseIdentityRG,
		@SpouseCPF
    )
END
GO


/****** Object:  StoredProcedure [dbo].[spDeleteContractUserProfile]    Script Date: 13/04/2021 17:40:15 ******/
SET ANSI_NULLS ON
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
GO


/****** Object:  StoredProcedure [dbo].[spGetAllAccounts]    Script Date: 13/04/2021 18:18:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Renters_Auth_GetRentersIdsOfAccount]
	@AccountId int
AS
BEGIN
    SELECT [Id]
	FROM Renters
	WHERE [Id] = [AccountId]
END
GO