--Drop columns FirstName and LastName
ALTER TABLE dbo.[RenteringUsers]
DROP COLUMN FirstName, LastName;

GO
--Creates StoredProcedure to CreateAccount for CUD Repostory without FirstName and LastName
SET ANSI_NULLS ON
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
    INSERT INTO [RenteringUsers] (
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
GO


--Creates StoredProcedure to GetAccountById for CUD Repository without FirstName and LastName
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Accounts_CUD_GetAccountById]
	@Id INT
AS
BEGIN
    SELECT RU.[Id], RU.[Email], RU.[Username], RU.[Password], RU.[Role]
	FROM RenteringUsers AS RU
	WHERE [Id] = @Id;
END
GO

--Creates StoredProcedure to UpdateAccount for CUD Repository without FirstName and LastName
SET ANSI_NULLS ON
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
		RenteringUsers
	SET 
		Email = @Email,
		Username = @Username,
		Password = @Password,
		Role = @Role
	WHERE 
		Id = @Id
END
GO



--Creates StoredProcedure to DeleteAccount for CUD Repository without FirstName and LastName
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Accounts_CUD_DeleteAccount]
	@Id INT
AS
BEGIN
	DELETE FROM 
		RenteringUsers
	WHERE 
		Id = @Id
END
GO


--Creates StoredProcedure to GetAllAccounts for CUD Repository without FirstName and LastName
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Accounts_CUD_GetAllAccounts]
AS
BEGIN
    SELECT AC.[Id], AC.[Email], AC.[Username], AC.[Password], AC.[Role]
	FROM RenteringUsers AS AC
END
GO

