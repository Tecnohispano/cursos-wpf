USE [CursosWPF]
GO

/****** Object:  Table [dbo].[User]    Script Date: 9/25/2020 12:18:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[HashedPassword] [varbinary](8000) NOT NULL,
	[Salt] [varbinary](8000) NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


