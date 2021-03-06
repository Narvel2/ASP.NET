USE [AdrianPiecykLaby5]
GO
/****** Object:  Table [dbo].[Connector]    Script Date: 10.12.2017 23:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connector](
	[CoursesID] [int] NOT NULL,
	[StudentsID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Connector_1] PRIMARY KEY CLUSTERED 
(
	[StudentsID] ASC,
	[CoursesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courses]    Script Date: 10.12.2017 23:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CoursesID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ECTSPoints] [nvarchar](50) NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CoursesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 10.12.2017 23:54:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentsID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[DateOfBirth] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CoursesID], [Name], [ECTSPoints]) VALUES (1, N'Algebra', N'3')
INSERT [dbo].[Courses] ([CoursesID], [Name], [ECTSPoints]) VALUES (2, N'Analiza Matematyczna', N'5')
SET IDENTITY_INSERT [dbo].[Courses] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentsID], [Name], [Surname], [DateOfBirth]) VALUES (2, N'Łukasz', N'Bieszczad', N'21041996')
INSERT [dbo].[Students] ([StudentsID], [Name], [Surname], [DateOfBirth]) VALUES (3, N'Cezary', N'Depta', N'29081999')
INSERT [dbo].[Students] ([StudentsID], [Name], [Surname], [DateOfBirth]) VALUES (4, N'Marek', N'Mostowiak', N'29081999')
SET IDENTITY_INSERT [dbo].[Students] OFF
ALTER TABLE [dbo].[Connector]  WITH CHECK ADD  CONSTRAINT [FK_Connector_Courses1] FOREIGN KEY([CoursesID])
REFERENCES [dbo].[Courses] ([CoursesID])
GO
ALTER TABLE [dbo].[Connector] CHECK CONSTRAINT [FK_Connector_Courses1]
GO
ALTER TABLE [dbo].[Connector]  WITH CHECK ADD  CONSTRAINT [FK_Connector_Students1] FOREIGN KEY([StudentsID])
REFERENCES [dbo].[Students] ([StudentsID])
GO
ALTER TABLE [dbo].[Connector] CHECK CONSTRAINT [FK_Connector_Students1]
GO
