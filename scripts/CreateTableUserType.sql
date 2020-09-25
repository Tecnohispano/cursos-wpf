USE [CursosWPF]
GO

/****** Object:  Table [dbo].[UserType]    Script Date: 9/25/2020 12:19:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserType](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]
GO


