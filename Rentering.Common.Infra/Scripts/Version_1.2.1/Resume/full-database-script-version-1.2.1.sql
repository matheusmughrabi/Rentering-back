GO
/****** Object:  Table [dbo].[ContractPayments]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[Month] [date] NOT NULL,
	[RenterPaymentStatus] [int] NOT NULL,
	[TenantPaymentStatus] [int] NOT NULL,
 CONSTRAINT [PK_ContractPayments_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractName] [nvarchar](50) NOT NULL,
	[RentPrice] [decimal](18, 10) NOT NULL,
	[RenterId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
 CONSTRAINT [PK_Contracts_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContractUserProfiles]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractUserProfiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RenteringUsers]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RenteringUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_RenteringUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContractPayments]  WITH CHECK ADD  CONSTRAINT [FK_ContractPayments_Contracts] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([Id])
GO
ALTER TABLE [dbo].[ContractPayments] CHECK CONSTRAINT [FK_ContractPayments_Contracts]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_ContractUserProfiles] FOREIGN KEY([RenterId])
REFERENCES [dbo].[ContractUserProfiles] ([Id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_ContractUserProfiles]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_ContractUserProfiles1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[ContractUserProfiles] ([Id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_ContractUserProfiles1]
GO
ALTER TABLE [dbo].[ContractUserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([Id])
REFERENCES [dbo].[ContractUserProfiles] ([Id])
GO
ALTER TABLE [dbo].[ContractUserProfiles] CHECK CONSTRAINT [FK_Users_Users]
GO
/****** Object:  StoredProcedure [dbo].[spAcceptPayment]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spAcceptPayment]
	@ContractId INT,
    @Month DATE
AS
BEGIN
	UPDATE 
		ContractPayments
	SET 
		RenterPaymentStatus = 2
	WHERE 
		ContractId = @ContractId
		AND
		Month = @Month
END
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfAccountExists]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCheckIfAccountExists]
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
/****** Object:  StoredProcedure [dbo].[spCheckIfContractExists]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCheckIfContractExists]
	@ContractId INT
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [Contracts]
		WHERE [Id] = @ContractId
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfContractNameExists]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spCheckIfContractNameExists]
	@ContractName NVARCHAR(50)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [Contracts]
		WHERE [ContractName] = @ContractName
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfDateIsAlreadyRegistered]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCheckIfDateIsAlreadyRegistered]
	@ContractId INT,
	@Month DATE
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [ContractPayments]
		WHERE [ContractId] = @ContractId AND [Month] = @Month
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfEmailExists]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCheckIfEmailExists]
	@Email NVARCHAR(50)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [RenteringUsers]
		WHERE [Email] = @Email
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfUsernameExists]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCheckIfUsernameExists]
	@Username NVARCHAR(50)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [RenteringUsers]
		WHERE [Username] = @Username
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END
GO
/****** Object:  StoredProcedure [dbo].[spCreateAccount]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateAccount]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(50),
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
	@Role int
AS
BEGIN
    INSERT INTO [RenteringUsers] (
        [FirstName], 
        [LastName], 
        [Email], 
        [Username], 
        [Password],
		[Role]
    ) VALUES (
        @FirstName,
        @LastName,
        @Email,
        @Username,
        @Password,
		@Role
    )
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateContract]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateContract]
    @ContractName NVARCHAR(50),
    @RentPrice DECIMAL(18,10),
    @RenterId INT,
    @TenantId INT
AS
BEGIN
    INSERT INTO [Contracts] (
        [ContractName], 
        [RentPrice], 
        [RenterId], 
        [TenantId]
    ) VALUES (
        @ContractName,
        @RentPrice,
        @RenterId,
        @TenantId
    )
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateContractPayment]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateContractPayment]
    @ContractId INT,
	@Month DATE,
	@RenterPaymentStatus int,
	@TenantPaymentStatus int
AS
BEGIN
    INSERT INTO [ContractPayments] (
        [ContractId], 
        [Month], 
        [RenterPaymentStatus], 
        [TenantPaymentStatus]
    ) VALUES (
        @ContractId,
        @Month,
        @RenterPaymentStatus,
        @TenantPaymentStatus
    )
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateContractProfileUser]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateContractProfileUser]
    @AccountId int
AS
BEGIN
    INSERT INTO [ContractUserProfiles] (
        [AccountId]
    ) VALUES (
        @AccountId
    )
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAccount]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDeleteAccount]
	@Id INT
AS
BEGIN
	DELETE FROM 
		RenteringUsers
	WHERE 
		Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteContract]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spDeleteContract]
	@Id INT
AS
BEGIN
    DELETE FROM
	  Contracts
	WHERE
	  Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteContractUserProfile]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDeleteContractUserProfile]
	@Id INT
AS
BEGIN
	DELETE FROM 
		ContractUserProfiles
	WHERE 
		Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spExecutePayment]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spExecutePayment]
	@ContractId INT,
    @Month DATE
AS
BEGIN
	UPDATE 
		ContractPayments
	SET 
		TenantPaymentStatus = 1
	WHERE 
		ContractId = @ContractId
		AND
		Month = @Month
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAccountById]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAccountById]
	@Id INT
AS
BEGIN
    SELECT RU.[Id], RU.[FirstName],  RU.[LastName], RU.[Email], RU.[Username], RU.[Password], RU.[Role]
	FROM RenteringUsers AS RU
	WHERE [Id] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllAccounts]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAllAccounts]
AS
BEGIN
    SELECT RU.[Id], RU.[FirstName],  RU.[LastName], RU.[Email], RU.[Username], RU.[Password], RU.[Role]
	FROM RenteringUsers AS RU
END
GO
/****** Object:  StoredProcedure [dbo].[spGetContractById]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetContractById]
	@Id INT
AS
BEGIN
    SELECT C.[ContractName],  C.[RentPrice], C.[RenterId], C.[TenantId]
	FROM Contracts AS C
	WHERE [Id] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetContractPaymentByContractIdAndMonth]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetContractPaymentByContractIdAndMonth]
	@ContractId INT,
	@Month Date
AS
BEGIN
    SELECT CP.[ContractId],  CP.[Month], CP.[RenterPaymentStatus], CP.[TenantPaymentStatus]
	FROM ContractPayments AS CP
	WHERE [ContractId] = @ContractId AND [Month] = @Month;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetContractUserProfileById]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetContractUserProfileById]
	@Id INT
AS
BEGIN
    SELECT U.[AccountId]
	FROM ContractUserProfiles AS U
	WHERE [Id] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spRejectPayment]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spRejectPayment]
	@ContractId INT,
    @Month DATE
AS
BEGIN
	UPDATE 
		ContractPayments
	SET 
		RenterPaymentStatus = 1
	WHERE 
		ContractId = @ContractId
		AND
		Month = @Month
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateAccount]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateAccount]
	@Id INT,
    @FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Username NVARCHAR(50),
	@Password NVARCHAR(50),
	@Role int
AS
BEGIN
	UPDATE 
		RenteringUsers
	SET 
		FirstName = @FirstName,
		LastName = @LastName,
		Email = @Email,
		Username = @Username,
		Password = @Password,
		Role = @Role
	WHERE 
		Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateContractRentPrice]    Script Date: 05/04/2021 22:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateContractRentPrice]
	@Id INT,
	@RentPrice DECIMAL(18,10)
AS
BEGIN
	UPDATE 
		Contracts
	SET 
		RentPrice = @RentPrice
	WHERE 
		Id = @Id
END
GO

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


