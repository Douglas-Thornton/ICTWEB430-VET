USE [master]
GO

CREATE LOGIN [VetWebAppLogin] WITH PASSWORD=N'cjt5jnc!cdm7WQV.qwp', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
-- Create the database
CREATE DATABASE [VETDB];
GO

USE [VETDB]
GO
/****** Object:  User [VetWebAppUser]    Script Date: 19/09/2023 6:17:30 PM ******/
CREATE USER [VetWebAppUser] FOR LOGIN [VetWebAppLogin] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE [db_datareader] ADD MEMBER [VetWebAppUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [VetWebAppUser]
GO

/****** Object:  Table [dbo].[AppPreferences]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppPreferences](
	[UserID] [int] NOT NULL,
	[WebpageAnimalPreference] [varchar](255) NULL,
	[SelectedFont] [varchar](255) NULL,
	[SelectedFontSize] [varchar](255) NULL,
 CONSTRAINT [PK_AppPreferences] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvitedPet]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitedPet](
	[InvitedPetID] [int] IDENTITY(1,1) NOT NULL,
	[PetID] [int] NOT NULL,
	[MeetingID] [int] NOT NULL,
	[InviteID] [int] NOT NULL,
 CONSTRAINT [PK__InvitedP__BA74B7723B9CC644] PRIMARY KEY CLUSTERED 
(
	[InvitedPetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvitedUser]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitedUser](
	[InviteID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MeetingID] [int] NOT NULL,
	[Accepted] [bit] NULL,
	[ResponseDate] [datetime] NULL,
 CONSTRAINT [PK__InvitedU__AFACE80D549C1825] PRIMARY KEY CLUSTERED 
(
	[InviteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[MeetingID] [int] IDENTITY(1,1) NOT NULL,
	[UserCreated] [int] NOT NULL,
	[MeetingDate] [datetime] NULL,
	[MeetingLocation] [varchar](255) NULL,
	[MeetingCreationDate] [datetime] NULL,
	[MeetingCancellationDate] [datetime] NULL,
	[MeetingName] [varchar](255) NULL,
	[MeetingMessage] [varchar](max) NULL,
 CONSTRAINT [PK__Meeting__E9F9E9AC6ED1A5A1] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[PetID] [int] IDENTITY(1,1) NOT NULL,
	[OwnerID] [int] NOT NULL,
	[PetName] [varchar](255) NULL,
	[PetBreed] [varchar](255) NULL,
	[PetAge] [int] NULL,
	[PetGender] [varchar](10) NULL,
	[PetDiscoverability] [bit] NOT NULL,
	[PetPhoto] [varbinary](max) NULL,
	[PetPhotoFileLocation] [varchar](255) NULL,
 CONSTRAINT [PK__Pet__48E53802F8676B27] PRIMARY KEY CLUSTERED 
(
	[PetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/09/2023 6:17:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[Surname] [varchar](255) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [varchar](255) NULL,
	[Suburb] [varchar](255) NULL,
	[Postcode] [varchar](10) NULL,
	[LoginUsername] [varchar](255) NOT NULL,
	[LoginPassword] [varchar](255) NOT NULL,
 CONSTRAINT [PK__User__1788CCAC593A0EDB] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_LoginUsername] UNIQUE NONCLUSTERED 
(
	[LoginUsername] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppPreferences]  WITH CHECK ADD  CONSTRAINT [FK_AppPreferences_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[AppPreferences] CHECK CONSTRAINT [FK_AppPreferences_User]
GO
ALTER TABLE [dbo].[InvitedPet]  WITH CHECK ADD  CONSTRAINT [FK__InvitedPe__Invit__31EC6D26] FOREIGN KEY([InviteID])
REFERENCES [dbo].[InvitedUser] ([InviteID])
GO
ALTER TABLE [dbo].[InvitedPet] CHECK CONSTRAINT [FK__InvitedPe__Invit__31EC6D26]
GO
ALTER TABLE [dbo].[InvitedPet]  WITH CHECK ADD  CONSTRAINT [FK__InvitedPe__Meeti__30F848ED] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[InvitedPet] CHECK CONSTRAINT [FK__InvitedPe__Meeti__30F848ED]
GO
ALTER TABLE [dbo].[InvitedPet]  WITH CHECK ADD  CONSTRAINT [FK__InvitedPe__PetID__300424B4] FOREIGN KEY([PetID])
REFERENCES [dbo].[Pet] ([PetID])
GO
ALTER TABLE [dbo].[InvitedPet] CHECK CONSTRAINT [FK__InvitedPe__PetID__300424B4]
GO
ALTER TABLE [dbo].[InvitedUser]  WITH CHECK ADD  CONSTRAINT [FK__InvitedUs__Meeti__2D27B809] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[InvitedUser] CHECK CONSTRAINT [FK__InvitedUs__Meeti__2D27B809]
GO
ALTER TABLE [dbo].[InvitedUser]  WITH CHECK ADD  CONSTRAINT [FK__InvitedUs__UserI__2C3393D0] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[InvitedUser] CHECK CONSTRAINT [FK__InvitedUs__UserI__2C3393D0]
GO
ALTER TABLE [dbo].[Meeting]  WITH CHECK ADD  CONSTRAINT [FK__Meeting__UserCre__29572725] FOREIGN KEY([UserCreated])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Meeting] CHECK CONSTRAINT [FK__Meeting__UserCre__29572725]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK__Pet__OwnerID__267ABA7A] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK__Pet__OwnerID__267ABA7A]
GO

