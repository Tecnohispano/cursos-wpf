USE [CursosWPF]
GO

/****** Object:  Table [dbo].[RelUserCourse]    Script Date: 9/25/2020 12:20:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RelUserCourse](
	[RelUserCourseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL
) ON [PRIMARY]
GO


