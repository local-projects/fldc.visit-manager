USE [master]
GO
/****** Object:  Database [FLDCVisitManager]    Script Date: 4/13/2020 3:04:42 PM ******/
CREATE DATABASE [FLDCVisitManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FLDCVisitManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FLDCVisitManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FLDCVisitManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FLDCVisitManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FLDCVisitManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FLDCVisitManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FLDCVisitManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FLDCVisitManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FLDCVisitManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FLDCVisitManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FLDCVisitManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET RECOVERY FULL 
GO
ALTER DATABASE [FLDCVisitManager] SET  MULTI_USER 
GO
ALTER DATABASE [FLDCVisitManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FLDCVisitManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FLDCVisitManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FLDCVisitManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FLDCVisitManager] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FLDCVisitManager', N'ON'
GO
ALTER DATABASE [FLDCVisitManager] SET QUERY_STORE = OFF
GO
USE [FLDCVisitManager]
GO
/****** Object:  User [TAMARZANZOURIDE\tamar]    Script Date: 4/13/2020 3:04:42 PM ******/
CREATE USER [TAMARZANZOURIDE\tamar] FOR LOGIN [TAMARZANZOURIDE\tamar] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[CollectionPoints]    Script Date: 4/13/2020 3:04:42 PM ******/
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
/****** Object:  Table [dbo].[FirmwareFTPDetails]    Script Date: 4/13/2020 3:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FirmwareFTPDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FtpServer] [nvarchar](50) NOT NULL,
	[FtpPath] [nvarchar](200) NOT NULL,
	[FtpFileName] [nvarchar](200) NOT NULL,
	[FtpUserName] [nvarchar](50) NOT NULL,
	[FtpPw] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FirmwareFTPDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lamps]    Script Date: 4/13/2020 3:04:42 PM ******/
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
/****** Object:  Table [dbo].[Status]    Script Date: 4/13/2020 3:04:42 PM ******/
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
/****** Object:  Table [dbo].[Visitors]    Script Date: 4/13/2020 3:04:42 PM ******/
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
/****** Object:  Table [dbo].[VisitorToCollection]    Script Date: 4/13/2020 3:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorToCollection](
	[VisitorId] [int] NOT NULL,
	[CollectionPointId] [nvarchar](10) NOT NULL,
	[AssetId] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastUpdated] [datetime] NOT NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_VisitorToCollection] PRIMARY KEY CLUSTERED 
(
	[VisitorId] ASC,
	[CollectionPointId] ASC,
	[AssetId] ASC
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
/****** Object:  StoredProcedure [dbo].[stp_ChargerDockerLamp_Update]    Script Date: 4/13/2020 3:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_ChargerDockerLamp_Update]
(
	@LampId nvarchar(10),
	@Port int,
	@Status nvarchar(50),
	@FwVersion nvarchar(50), 
	@SeqVersion nvarchar(50) 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @StatusId int = 0
	select @StatusId = Id from [Status] where [Status].Description = @Status
	
	UPDATE Lamps
	set ChargerPort = @Port,
	Firmware = @FwVersion,
	SeqVersion = @SeqVersion,
	LampStatus = @StatusId
	where Id = @LampId

END
GO
/****** Object:  StoredProcedure [dbo].[stp_CollectionPoint_Update]    Script Date: 4/13/2020 3:04:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[stp_FirmwareFtpDetails_Get]    Script Date: 4/13/2020 3:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_FirmwareFtpDetails_Get] 
(
	@ProductName nvarchar(50) = 'Collection point'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT FtpServer, FtpPath, FtpFileName, FtpUserName, FtpPw FROM FirmwareFTPDetails
	where ProductName = @ProductName

END
GO
/****** Object:  StoredProcedure [dbo].[stp_VisitorCollections_Get]    Script Date: 4/13/2020 3:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tamar Zanzouri
-- Create date: 7/4/2020
-- Description:	get visitor's collections
-- =============================================
CREATE PROCEDURE [dbo].[stp_VisitorCollections_Get]
	@LampId nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT AssetId FROM VisitorToCollection
	INNER JOIN Visitors ON VisitorId = Visitors.Id
	WHERE LampId = @LampId AND Visitors.DateDeleted IS NULL

END
GO
/****** Object:  StoredProcedure [dbo].[stp_VisitorToCollectionPoint_Insert]    Script Date: 4/13/2020 3:04:42 PM ******/
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
	@LampId nvarchar(10),
	@AssetId nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Insert into VisitorToCollection 
	(
		CollectionPointId,
		VisitorId,
		AssetId
	)
	Select @CPId, Visitors.Id, @AssetId from Visitors 
	where LampId = @LampId and DateDeleted is null

END
GO
USE [master]
GO
ALTER DATABASE [FLDCVisitManager] SET  READ_WRITE 
GO
