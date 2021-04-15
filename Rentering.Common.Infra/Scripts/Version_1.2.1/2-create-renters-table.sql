/****** Object:  Table [dbo].[Renters]    Script Date: 14/04/2021 22:35:48 ******/
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
	[SpouseFirstName] [nvarchar](50) NULL,
	[SpouseLastName] [nvarchar](50) NULL,
	[SpouseNationality] [nvarchar](50) NULL,
	[SpouseIdentityRG] [nvarchar](50) NULL,
	[SpouseCPF] [nvarchar](50) NULL,
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


