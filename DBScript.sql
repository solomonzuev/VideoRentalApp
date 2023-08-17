USE [master]
GO
/****** Object:  Database [VideoRentalDB]    Script Date: 17.08.2023 13:31:13 ******/
CREATE DATABASE [VideoRentalDB]
GO
ALTER DATABASE [VideoRentalDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VideoRentalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VideoRentalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VideoRentalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VideoRentalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VideoRentalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VideoRentalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [VideoRentalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VideoRentalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VideoRentalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VideoRentalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VideoRentalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VideoRentalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VideoRentalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VideoRentalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VideoRentalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VideoRentalDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VideoRentalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VideoRentalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VideoRentalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VideoRentalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VideoRentalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VideoRentalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VideoRentalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VideoRentalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VideoRentalDB] SET  MULTI_USER 
GO
ALTER DATABASE [VideoRentalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VideoRentalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VideoRentalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VideoRentalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VideoRentalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VideoRentalDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VideoRentalDB] SET QUERY_STORE = OFF
GO
USE [VideoRentalDB]
GO
/****** Object:  Table [dbo].[ActorsInVideos]    Script Date: 17.08.2023 13:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorsInVideos](
	[FilmId] [int] NOT NULL,
	[ActorId] [int] NOT NULL,
 CONSTRAINT [PK_ActorsInVideos] PRIMARY KEY CLUSTERED 
(
	[FilmId] ASC,
	[ActorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Phone] [char](12) NULL,
	[UserId] [int] NULL,
	[InBlackList] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Phone] [char](12) NULL,
	[UserId] [int] NULL,
	[PositionId] [int] NULL,
	[StoreId] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilmCredits]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilmCredits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Sex] [nchar](1) NULL,
	[Height] [smallint] NULL,
	[Birthdate] [date] NULL,
	[Deathdate] [date] NULL,
 CONSTRAINT [PK_VideoCelebrities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Films]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Films](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[GenreId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[DirectorId] [int] NOT NULL,
	[ReleaseDate] [date] NOT NULL,
	[LimitAge] [tinyint] NOT NULL,
	[Price3Days] [money] NOT NULL,
	[PosterPath] [nvarchar](255) NULL,
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilmsInMedia]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilmsInMedia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilmId] [int] NOT NULL,
	[MediaTypeId] [int] NOT NULL,
	[Units] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [PK__VideosIn__3214EC07143A6298] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediaTypes]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediaTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Salary] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreLocations]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreLocations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[OpeningTime] [time](7) NOT NULL,
	[ClosingTime] [time](7) NOT NULL,
 CONSTRAINT [PK_StoreLocations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilmId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsIssuied] [bit] NOT NULL,
	[VideosInMediaId] [int] NOT NULL,
	[TotalPrice] [money] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.08.2023 14:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([Id], [FullName], [UserId], [PositionId]) VALUES (1, N'Иванов Иван Иванович', 1, 5)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Боевик')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Драма')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Мелодрама')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Комедия')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (5, N'Вестерн')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (6, N'Исторический')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (7, N'Научная фантастика')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (8, N'Фэнтези')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (9, N'Детектив')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (10, N'Ужасы')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (11, N'Нуар')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (12, N'Трагедия')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (13, N'Трагикомедия')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (14, N'Триллер')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (15, N'Приключенческий')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (16, N'Приключенческий триллер')
GO
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (17, N'Криминал')
GO
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[MediaTypes] ON 
GO
INSERT [dbo].[MediaTypes] ([Id], [Name]) VALUES (1, N'Blu-Ray')
GO
INSERT [dbo].[MediaTypes] ([Id], [Name]) VALUES (2, N'DVD')
GO
INSERT [dbo].[MediaTypes] ([Id], [Name]) VALUES (3, N'Цифровой код')
GO
SET IDENTITY_INSERT [dbo].[MediaTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Positions] ON 
GO
INSERT [dbo].[Positions] ([Id], [Name], [Salary]) VALUES (1, N'Продавец бытовой техники', 40000.0000)
GO
INSERT [dbo].[Positions] ([Id], [Name], [Salary]) VALUES (2, N'Менеджер по продажам', 50000.0000)
GO
INSERT [dbo].[Positions] ([Id], [Name], [Salary]) VALUES (3, N'Кассир', 30000.0000)
GO
INSERT [dbo].[Positions] ([Id], [Name], [Salary]) VALUES (4, N'Управляющий магазина', 80000.0000)
GO
INSERT [dbo].[Positions] ([Id], [Name], [Salary]) VALUES (5, N'Администратор', 45000.0000)
GO
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (1, N'adm1@example.com', N'admin')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT ((0)) FOR [InBlackList]
GO
ALTER TABLE [dbo].[FilmsInMedia] ADD  CONSTRAINT [DF_VideosInMedia_IsAvaliable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_StartDate]  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF__Transacti__IsIss__625A9A57]  DEFAULT ((0)) FOR [IsIssuied]
GO
ALTER TABLE [dbo].[ActorsInVideos]  WITH CHECK ADD  CONSTRAINT [FK_ActorsInVideos_VideoParticipants] FOREIGN KEY([ActorId])
REFERENCES [dbo].[FilmCredits] ([Id])
GO
ALTER TABLE [dbo].[ActorsInVideos] CHECK CONSTRAINT [FK_ActorsInVideos_VideoParticipants]
GO
ALTER TABLE [dbo].[ActorsInVideos]  WITH CHECK ADD  CONSTRAINT [FK_ActorsInVideos_Videos] FOREIGN KEY([FilmId])
REFERENCES [dbo].[Films] ([Id])
GO
ALTER TABLE [dbo].[ActorsInVideos] CHECK CONSTRAINT [FK_ActorsInVideos_Videos]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK__Employees__UserI__6166761E] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK__Employees__UserI__6166761E]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Positions]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_StoreLocations] FOREIGN KEY([StoreId])
REFERENCES [dbo].[StoreLocations] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_StoreLocations]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [FK_Videos_Genres] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [FK_Videos_Genres]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [FK_Videos_VideoCelebrities] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[FilmCredits] ([Id])
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [FK_Videos_VideoCelebrities]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [FK_Videos_VideoCelebrities1] FOREIGN KEY([DirectorId])
REFERENCES [dbo].[FilmCredits] ([Id])
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [FK_Videos_VideoCelebrities1]
GO
ALTER TABLE [dbo].[FilmsInMedia]  WITH CHECK ADD  CONSTRAINT [FK__VideosInM__Media__55009F39] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MediaTypes] ([Id])
GO
ALTER TABLE [dbo].[FilmsInMedia] CHECK CONSTRAINT [FK__VideosInM__Media__55009F39]
GO
ALTER TABLE [dbo].[FilmsInMedia]  WITH CHECK ADD  CONSTRAINT [FK__VideosInM__Video__540C7B00] FOREIGN KEY([FilmId])
REFERENCES [dbo].[Films] ([Id])
GO
ALTER TABLE [dbo].[FilmsInMedia] CHECK CONSTRAINT [FK__VideosInM__Video__540C7B00]
GO
ALTER TABLE [dbo].[FilmsInMedia]  WITH CHECK ADD  CONSTRAINT [FK_VideosInMedia_StoreLocations] FOREIGN KEY([StoreId])
REFERENCES [dbo].[StoreLocations] ([Id])
GO
ALTER TABLE [dbo].[FilmsInMedia] CHECK CONSTRAINT [FK_VideosInMedia_StoreLocations]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Customers]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_FilmsInMedia] FOREIGN KEY([VideosInMediaId])
REFERENCES [dbo].[FilmsInMedia] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_FilmsInMedia]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Videos] FOREIGN KEY([FilmId])
REFERENCES [dbo].[Films] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Videos]
GO
ALTER TABLE [dbo].[FilmCredits]  WITH CHECK ADD  CONSTRAINT [CK_Height_VideoParticipants] CHECK  (([Height]>=(50) AND [Height]<=(300)))
GO
ALTER TABLE [dbo].[FilmCredits] CHECK CONSTRAINT [CK_Height_VideoParticipants]
GO
ALTER TABLE [dbo].[FilmCredits]  WITH CHECK ADD  CONSTRAINT [CK_Sex_VideoParticipants] CHECK  (([Sex]=N'ж' OR [Sex]=N'м' OR [Sex]=N'Ж' OR [Sex]=N'М'))
GO
ALTER TABLE [dbo].[FilmCredits] CHECK CONSTRAINT [CK_Sex_VideoParticipants]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [CK_LimitAge_Videos] CHECK  (([LimitAge]>=(0) AND [LimitAge]<=(120)))
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [CK_LimitAge_Videos]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [CK_Price3Days_Videos] CHECK  (([Price3Days]>=(0)))
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [CK_Price3Days_Videos]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [CK_Transactions] CHECK  (([EndDate]>[StartDate]))
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [CK_Transactions]
GO
