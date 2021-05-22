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

/****** Object:  Table [dbo].[Renters]    Script Date: 12/04/2021 23:05:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Renters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Nationality] [nvarchar](50) NOT NULL,
	[Ocupation] [nvarchar](50) NOT NULL,
	[MaritalStatus] [int] NOT NULL,
	[IdentityRG] [nvarchar](50) NOT NULL,
	[CPF] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Bairro] [nvarchar](50) NOT NULL,
	[Cidade] [nvarchar](50) NOT NULL,
	[CEP] [nvarchar](50) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[SpouseFirstName] [nvarchar](50) NOT NULL,
	[SpouseLastName] [nvarchar](50) NOT NULL,
	[SpouseNationality] [nvarchar](50) NOT NULL,
	[SpouseIdentityRG] [nvarchar](50) NOT NULL,
	[SpouseCPF] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Renters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Renters]  WITH CHECK ADD  CONSTRAINT [FK_Renters_RenteringUsers] FOREIGN KEY([AccountId])
REFERENCES [dbo].[RenteringUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Renters] CHECK CONSTRAINT [FK_Renters_RenteringUsers]
GO


