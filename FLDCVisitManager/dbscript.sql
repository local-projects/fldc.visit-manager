USE [FLDCVisitManager]
GO
/****** Object:  Table [dbo].[CollectionPoints]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionPoints](
	[Id] [nvarchar](10) NOT NULL,
	[CPSN] [nvarchar](100) NOT NULL,
	[Firmware] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastUpdated] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_CollectionPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lamps]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lamps](
	[Id] [nvarchar](10) NOT NULL,
	[LampSN] [nvarchar](100) NOT NULL,
	[LampStatus] [int] NOT NULL,
	[ChargerPort] [int] NULL,
	[Firmware] [nvarchar](50) NULL,
	[SeqVersion] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastUpdated] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_Lamps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visitors]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visitors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Ticket] [nvarchar](200) NOT NULL,
	[LampId] [nvarchar](10) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_Visitors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitorToCollection]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorToCollection](
	[VisitorId] [int] NOT NULL,
	[CollectionPointId] [nvarchar](10) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastUpdated] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_VisitorToCollection] PRIMARY KEY CLUSTERED 
(
	[VisitorId] ASC,
	[CollectionPointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CollectionPoints] ADD  CONSTRAINT [DF_CollectionPoints_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CollectionPoints] ADD  CONSTRAINT [DF_CollectionPoints_DateLastUpdated]  DEFAULT (getdate()) FOR [DateLastUpdated]
GO
ALTER TABLE [dbo].[Lamps] ADD  CONSTRAINT [DF_Lamps_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Lamps] ADD  CONSTRAINT [DF_Lamps_DateLastUpdated]  DEFAULT (getdate()) FOR [DateLastUpdated]
GO
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_DateLastModified]  DEFAULT (getdate()) FOR [DateLastModified]
GO
ALTER TABLE [dbo].[Visitors] ADD  CONSTRAINT [DF_Visitor_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Visitors] ADD  CONSTRAINT [DF_Visitor_DateLastModified]  DEFAULT (getdate()) FOR [DateLastModified]
GO
ALTER TABLE [dbo].[VisitorToCollection] ADD  CONSTRAINT [DF_VisitorToCollection_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[VisitorToCollection] ADD  CONSTRAINT [DF_VisitorToCollection_DateLastUpdated]  DEFAULT (getdate()) FOR [DateLastUpdated]
GO
ALTER TABLE [dbo].[CollectionPoints]  WITH CHECK ADD  CONSTRAINT [FK_CollectionPoints_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[CollectionPoints] CHECK CONSTRAINT [FK_CollectionPoints_Status]
GO
ALTER TABLE [dbo].[Lamps]  WITH CHECK ADD  CONSTRAINT [FK_Lamps_Status] FOREIGN KEY([LampStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Lamps] CHECK CONSTRAINT [FK_Lamps_Status]
GO
ALTER TABLE [dbo].[Visitors]  WITH CHECK ADD  CONSTRAINT [FK_Visitors_Lamps] FOREIGN KEY([LampId])
REFERENCES [dbo].[Lamps] ([Id])
GO
ALTER TABLE [dbo].[Visitors] CHECK CONSTRAINT [FK_Visitors_Lamps]
GO
ALTER TABLE [dbo].[VisitorToCollection]  WITH CHECK ADD  CONSTRAINT [FK_CollectionPoint_VisitorToCollectionPoint] FOREIGN KEY([CollectionPointId])
REFERENCES [dbo].[CollectionPoints] ([Id])
GO
ALTER TABLE [dbo].[VisitorToCollection] CHECK CONSTRAINT [FK_CollectionPoint_VisitorToCollectionPoint]
GO
ALTER TABLE [dbo].[VisitorToCollection]  WITH CHECK ADD  CONSTRAINT [FK_Visitors_VisitorToCollection] FOREIGN KEY([VisitorId])
REFERENCES [dbo].[Visitors] ([Id])
GO
ALTER TABLE [dbo].[VisitorToCollection] CHECK CONSTRAINT [FK_Visitors_VisitorToCollection]
GO
/****** Object:  StoredProcedure [dbo].[stp_CollectionPoint_Update]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tamar Zanzouri	
-- Create date: 3-27-2020
-- Description:	update cp heart beat and firmware
-- =============================================
CREATE PROCEDURE [dbo].[stp_CollectionPoint_Update] 
(
	@CPId nvarchar(10),
	@CPFw nvarchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   Update CollectionPoints
   set Firmware = @CPFw
   where Id = @CPId

END
GO
/****** Object:  StoredProcedure [dbo].[stp_VisitorToCollectionPoint_Insert]    Script Date: 3/27/2020 10:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tamar Zanzouri
-- Create date: 3-26-2020
-- Description:	insert visitor link to collection point
-- =============================================
CREATE PROCEDURE [dbo].[stp_VisitorToCollectionPoint_Insert]
(
	@CPId nvarchar(10), 
	@LampId nvarchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Insert into VisitorToCollection 
	(
		CollectionPointId,
		VisitorId
	)
	Select @CPId, Visitors.Id from Visitors 
	where LampId = @LampId and DateDeleted is null

END
GO
