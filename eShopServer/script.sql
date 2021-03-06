USE [master]
GO
/****** Object:  Database [eShop]    Script Date: 1/15/2019 4:42:16 PM ******/
CREATE DATABASE [eShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eShop', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\eShop.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'eShop_log', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\eShop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [eShop] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [eShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [eShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [eShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [eShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [eShop] SET  MULTI_USER 
GO
ALTER DATABASE [eShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [eShop] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'eShop', N'ON'
GO
USE [eShop]
GO
/****** Object:  Table [dbo].[BrandBar]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandBar](
	[BrandBarItemID] [int] IDENTITY(1,1) NOT NULL,
	[BrandID] [int] NOT NULL,
 CONSTRAINT [PK_BrandBar] PRIMARY KEY CLUSTERED 
(
	[BrandBarItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Brands]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[ImageID] [int] NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HomeSlider]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeSlider](
	[HomeSliderItemID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_HomeSlider] PRIMARY KEY CLUSTERED 
(
	[HomeSliderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRole](
	[Id] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRoleClaim`1]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRoleClaim`1](
	[Id] [int] NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserClaim`1]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserClaim`1](
	[Id] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogin`1]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogin`1](
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserRole`1]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole`1](
	[UserId] [nvarchar](max) NULL,
	[RoleId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserToken`1]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserToken`1](
	[UserId] [nvarchar](max) NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BrandID] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Price] [int] NOT NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[Availability] [int] NOT NULL,
	[Description] [text] NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_Products_Status]  DEFAULT ((1)),
	[ImageID] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/15/2019 4:42:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[BrandBar] ON 

INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (1, 1)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (2, 5)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (3, 6)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (4, 4)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (5, 7)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (6, 8)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (7, 2)
INSERT [dbo].[BrandBar] ([BrandBarItemID], [BrandID]) VALUES (8, 9)
SET IDENTITY_INSERT [dbo].[BrandBar] OFF
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (1, N'Beta', 74)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (2, N'Drapper', 75)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (3, N'Metabo', 76)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (4, N'GYS', 77)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (5, N'IPC', 78)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (6, N'Proxxon', 79)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (7, N'Big Red', 80)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (8, N'Lechler', 81)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (9, N'F.lli Bonezzi', 82)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (18, N'1323123', 92)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (19, N'Company Name', 94)
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (1, N'Automitive', NULL)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (2, N'Jewlery', NULL)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (3, N'Industrial', NULL)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (4, N'Solar', NULL)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (5, N'Car Body Repair', 1)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (6, N'Car Wash Equipment', 1)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (7, N'Chargers / Starters', 1)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (8, N'Hand Tools / Sockets', 1)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (9, N'Bit', 3)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (10, N'Electric Tools & Equipment ', 3)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (11, N'Micromotor Tools & Equipment', 3)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (12, N'Furnaces / Consumables ', 2)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (13, N'Laser & Contact Welders / Consumables', 2)
INSERT [dbo].[Categories] ([CategoryID], [Name], [ParentID]) VALUES (14, N'Marking / Engraving Equipment', 2)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[HomeSlider] ON 

INSERT [dbo].[HomeSlider] ([HomeSliderItemID], [Image], [Description]) VALUES (1, N'assets/images/slider/09_Proxxon.jpg', N'Proxxon - The fine tool company!')
INSERT [dbo].[HomeSlider] ([HomeSliderItemID], [Image], [Description]) VALUES (2, N'assets/images/slider/12Solar.jpg', NULL)
INSERT [dbo].[HomeSlider] ([HomeSliderItemID], [Image], [Description]) VALUES (3, N'assets/images/slider/02_IPC.jpg', NULL)
SET IDENTITY_INSERT [dbo].[HomeSlider] OFF
INSERT [dbo].[IdentityRole] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'438a3720-91ee-45c6-8d37-282472997695', N'Client', N'CLIENT', N'170ebfd3-fde2-4c3b-91c7-1c5bfabb9fd6')
INSERT [dbo].[IdentityRole] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'4dd3d406-cefa-4ba7-afbb-c3b722e8784f', N'Administrator', N'ADMINISTRATOR', N'7f69aaeb-41c5-4f8a-bd11-a5b45f307ff3')
INSERT [dbo].[IdentityRole] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c253b9f3-7e79-4f9f-91ed-d566bd65fd84', N'Moderator', N'MODERATOR', N'7c4c2aec-09d7-460a-9e7d-03af09099b1a')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'c340a8c9-a701-4c76-95c8-838f0929f4f2', N'4dd3d406-cefa-4ba7-afbb-c3b722e8784f')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'c340a8c9-a701-4c76-95c8-838f0929f4f2', N'438a3720-91ee-45c6-8d37-282472997695')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'c340a8c9-a701-4c76-95c8-838f0929f4f2', N'c253b9f3-7e79-4f9f-91ed-d566bd65fd84')
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (30, N'assets\images\ab18f179-4ce6-42cf-bef8-285880444fce.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (73, N'assets\images\83640db8-1841-440a-9d85-655502e71598.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (74, N'assets/brands/beta.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (75, N'assets/brands/draper.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (76, N'assets/brands/beta.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (77, N'assets/brands/gys.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (78, N'assets/brands/ipc.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (79, N'assets/brands/proxxon.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (80, N'assets/brands/bigred.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (81, N'assets/brands/lechler.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (82, N'assets/brands/bonezzi.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (92, N'assets\images\a39ff991-bb55-4467-add7-5796810ca425.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (93, N'assets\images\1f69ed83-b690-428d-a729-c774d56a6712.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (94, N'assets\images\28cdbdc5-c9fa-471a-9ede-579d2a2c8cc2.png')
SET IDENTITY_INSERT [dbo].[Images] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Name], [Price], [ProductCode], [Availability], [Description], [Status], [ImageID]) VALUES (27, 5, 1, N'Orange Juice', 750, N'4584558', 1, N'Juice', 0, 30)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Name], [Price], [ProductCode], [Availability], [Description], [Status], [ImageID]) VALUES (49, 9, 4, N'Headphones', 150000, N'4584523', 2, N'Headphones by Beats', 0, 73)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Name], [Price], [ProductCode], [Availability], [Description], [Status], [ImageID]) VALUES (51, 14, 6, N'Juice Pack', 520, N'5523562', 1, N'Juice Pack Description', 0, 93)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c340a8c9-a701-4c76-95c8-838f0929f4f2', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEHB7AElIZBvx/3ptT+qnVHtjihQC0Z/uyT1e+yq+3Dvandusace++hC7PbeIPua68A==', N'KQ6AMAD4J6I6KEL5QORJK4IJHTCQMHS7', N'bb3d6609-dc68-42d0-ac0b-c1222eb81bda', NULL, 0, 0, NULL, 1, 0)
ALTER TABLE [dbo].[BrandBar]  WITH CHECK ADD  CONSTRAINT [FK_BrandBar_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([BrandID])
GO
ALTER TABLE [dbo].[BrandBar] CHECK CONSTRAINT [FK_BrandBar_Brands]
GO
ALTER TABLE [dbo].[Brands]  WITH CHECK ADD  CONSTRAINT [FK_Brands_Images] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Images] ([ImageID])
GO
ALTER TABLE [dbo].[Brands] CHECK CONSTRAINT [FK_Brands_Images]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([BrandID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands]
GO
ALTER TABLE [dbo].[Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Images] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Images] ([ImageID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Images]
GO
USE [master]
GO
ALTER DATABASE [eShop] SET  READ_WRITE 
GO
