USE [master]
GO
/****** Object:  Database [Kennel]    Script Date: 9/22/2012 5:50:47 PM ******/
CREATE DATABASE [Kennel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Kennel', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Kennel.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Kennel_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Kennel_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Kennel] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kennel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kennel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kennel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kennel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kennel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kennel] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kennel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Kennel] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Kennel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kennel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kennel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kennel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kennel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kennel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kennel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kennel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kennel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kennel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kennel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kennel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kennel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kennel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kennel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kennel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kennel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Kennel] SET  MULTI_USER 
GO
ALTER DATABASE [Kennel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kennel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kennel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kennel] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Kennel]
GO
/****** Object:  Table [dbo].[Breeds]    Script Date: 9/22/2012 5:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Breeds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Breeds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dogs]    Script Date: 9/22/2012 5:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [int] NOT NULL,
	[BreedId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Legs] [decimal](18, 0) NOT NULL,
	[HasTail] [bit] NOT NULL,
 CONSTRAINT [PK_Dogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Owners]    Script Date: 9/22/2012 5:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Breeds] ON 

INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (1, N'Corgie')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (2, N'Dauschund')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (3, N'Golden Retriever')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (4, N'Mutt')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (5, N'Wild')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (6, N'Cat')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (7, N'Rotwiler')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (8, N'Stuffed')
INSERT [dbo].[Breeds] ([Id], [Name]) VALUES (9, N'Overweight')
SET IDENTITY_INSERT [dbo].[Breeds] OFF
SET IDENTITY_INSERT [dbo].[Dogs] ON 

INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (1, 1, 1, N'Spike', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (2, 1, 1, N'Spot', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (3, 1, 1, N'Skippy', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (4, 2, 1, N'Precious', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (5, 3, 1, N'Snoopy', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (6, 4, 1, N'Chuck', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (7, 5, 1, N'Brian', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (8, 6, 1, N'Shiva', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Dogs] ([Id], [OwnerId], [BreedId], [Name], [Legs], [HasTail]) VALUES (9, 6, 1, N'Manny', CAST(4 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Dogs] OFF
SET IDENTITY_INSERT [dbo].[Owners] ON 

INSERT [dbo].[Owners] ([Id], [Name]) VALUES (1, N'John')
INSERT [dbo].[Owners] ([Id], [Name]) VALUES (2, N'Tiffay')
INSERT [dbo].[Owners] ([Id], [Name]) VALUES (3, N'Aaron')
INSERT [dbo].[Owners] ([Id], [Name]) VALUES (4, N'Jasmine')
INSERT [dbo].[Owners] ([Id], [Name]) VALUES (5, N'Jared')
INSERT [dbo].[Owners] ([Id], [Name]) VALUES (6, N'Tamara')
SET IDENTITY_INSERT [dbo].[Owners] OFF
ALTER TABLE [dbo].[Dogs]  WITH CHECK ADD  CONSTRAINT [FK_Dogs_Breeds] FOREIGN KEY([BreedId])
REFERENCES [dbo].[Breeds] ([Id])
GO
ALTER TABLE [dbo].[Dogs] CHECK CONSTRAINT [FK_Dogs_Breeds]
GO
USE [master]
GO
ALTER DATABASE [Kennel] SET  READ_WRITE 
GO
