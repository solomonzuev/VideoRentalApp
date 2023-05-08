USE [master]
GO
/****** Object:  Database [VideoRentalDB]    Script Date: 08.05.2023 14:56:02 ******/
CREATE DATABASE [VideoRentalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VideoRentalDB', FILENAME = N'C:\Users\ИМЯ_ПОЛЬЗОВАТЕЛЯ\VideoRentalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VideoRentalDB_log', FILENAME = N'C:\Users\ИМЯ_ПОЛЬЗОВАТЕЛЯ\VideoRentalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
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
/****** Object:  Table [dbo].[ActorsInVideos]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Customers]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[FilmCredits]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Films]    Script Date: 08.05.2023 14:56:02 ******/
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
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilmsInMedia]    Script Date: 08.05.2023 14:56:02 ******/
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
	[IsAvaliable] [bit] NOT NULL,
 CONSTRAINT [PK__VideosIn__3214EC07143A6298] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[MediaTypes]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Positions]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[StoreLocations]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Transactions]    Script Date: 08.05.2023 14:56:02 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 08.05.2023 14:56:02 ******/
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
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1002, 1035)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1002, 1037)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1003, 1035)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1003, 1036)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1004, 1035)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1004, 1036)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1005, 1034)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1005, 1035)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1005, 1039)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1006, 1038)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1006, 1039)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1006, 1040)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1007, 1041)
GO
INSERT [dbo].[ActorsInVideos] ([FilmId], [ActorId]) VALUES (1007, 1042)
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [FullName], [Phone], [UserId], [InBlackList]) VALUES (1, N'Зябликов Семён Алексеевич', N'+79189003432', 1, 0)
GO
INSERT [dbo].[Customers] ([Id], [FullName], [Phone], [UserId], [InBlackList]) VALUES (3, N'Петрушкин Павел Миронович', N'+79109099877', 2, 1)
GO
INSERT [dbo].[Customers] ([Id], [FullName], [Phone], [UserId], [InBlackList]) VALUES (4, N'Скобелева Алиса Владимировна', N'+79087610332', 3, 0)
GO
INSERT [dbo].[Customers] ([Id], [FullName], [Phone], [UserId], [InBlackList]) VALUES (1006, N'Сергеев Иван Иванович', N'+79181734324', 1006, 0)
GO
INSERT [dbo].[Customers] ([Id], [FullName], [Phone], [UserId], [InBlackList]) VALUES (1007, N'Иванов Иван Иванович', N'+79234234234', 1007, 0)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([Id], [FullName], [Phone], [UserId], [PositionId], [StoreId]) VALUES (2, N'Иванов Иван Иванович', N'+79187656743', NULL, 2, 3)
GO
INSERT [dbo].[Employees] ([Id], [FullName], [Phone], [UserId], [PositionId], [StoreId]) VALUES (3, N'Петров Петр Петрович', N'+79187656411', NULL, 2, 3)
GO
INSERT [dbo].[Employees] ([Id], [FullName], [Phone], [UserId], [PositionId], [StoreId]) VALUES (5, N'Симонов Сергей Сергеевич', N'+79287766100', NULL, 2, 3)
GO
INSERT [dbo].[Employees] ([Id], [FullName], [Phone], [UserId], [PositionId], [StoreId]) VALUES (8, N'Михайлов Александр Сергеевич', N'+79208784561', NULL, 2, 3)
GO
INSERT [dbo].[Employees] ([Id], [FullName], [Phone], [UserId], [PositionId], [StoreId]) VALUES (9, N'Жуков Иван Павлович', N'+79181003001', NULL, 2, 3)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[FilmCredits] ON 
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1034, N'Джонни Депп', N'М', 178, CAST(N'1963-06-09' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1035, N'Леонардо Ди Каприо', N'М', 183, CAST(N'1974-11-11' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1036, N'Брэд Питт', N'М', 180, CAST(N'1963-12-18' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1037, N'Киану Ривз', N'М', 186, CAST(N'1964-09-02' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1038, N'Джена Ортега', N'Ж', 155, CAST(N'2002-09-27' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1039, N'Гвендолин Кристи', N'Ж', 191, CAST(N'1978-10-28' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1040, N'Эмма Майерс', N'Ж', 160, CAST(N'2002-04-02' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1041, N'Том Хэнкс', N'М', 183, CAST(N'1956-07-09' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1042, N'Майкл Кларк Дункан', N'М', 197, CAST(N'1957-12-10' AS Date), CAST(N'2012-09-03' AS Date))
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1043, N'Джон Фавро', N'М', 183, CAST(N'1966-10-19' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1044, N'Квентин Тарантино', N'М', 185, CAST(N'1963-03-27' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1045, N'Мартин Скорсезе', N'М', 163, CAST(N'1942-11-17' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1046, N'Гор Вербински', N'М', 185, CAST(N'1964-03-16' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1047, N'Тим Бёртон', N'М', 180, CAST(N'1958-08-25' AS Date), NULL)
GO
INSERT [dbo].[FilmCredits] ([Id], [FullName], [Sex], [Height], [Birthdate], [Deathdate]) VALUES (1048, N'Чарльз Аддамс', N'М', NULL, CAST(N'1912-01-07' AS Date), CAST(N'1988-08-29' AS Date))
GO
SET IDENTITY_INSERT [dbo].[FilmCredits] OFF
GO
SET IDENTITY_INSERT [dbo].[Films] ON 
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1002, N'Начало', 7, 1043, 1043, CAST(N'2010-07-22' AS Date), 12, 2000.0000)
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1003, N'Джанго освобожденный', 5, 1044, 1044, CAST(N'2012-12-25' AS Date), 18, 2500.0000)
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1004, N'Волк с Уолл-стрит', 4, 1045, 1045, CAST(N'2013-12-25' AS Date), 18, 4000.0000)
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1005, N'Пираты Карибского моря: Проклятие «Чёрной жемчужины»', 15, 1046, 1046, CAST(N'2003-06-28' AS Date), 16, 3000.0000)
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1006, N'Уэнсдей', 8, 1048, 1047, CAST(N'2022-10-31' AS Date), 18, 3500.0000)
GO
INSERT [dbo].[Films] ([Id], [Name], [GenreId], [AuthorId], [DirectorId], [ReleaseDate], [LimitAge], [Price3Days]) VALUES (1007, N'Зелёная миля', 2, 1046, 1046, CAST(N'2000-04-18' AS Date), 16, 2200.0000)
GO
SET IDENTITY_INSERT [dbo].[Films] OFF
GO
SET IDENTITY_INSERT [dbo].[FilmsInMedia] ON 
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (1, 1002, 1, 10, 1, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (2, 1002, 2, 20, 2, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (3, 1002, 3, 15, 3, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (4, 1003, 1, 5, 1, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (5, 1003, 2, 10, 2, 0)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (6, 1003, 3, 5, 3, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (7, 1004, 1, 10, 1, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (8, 1004, 2, 15, 2, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (9, 1004, 3, 5, 3, 0)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (10, 1005, 1, 5, 1, 0)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (11, 1005, 2, 5, 2, 1)
GO
INSERT [dbo].[FilmsInMedia] ([Id], [FilmId], [MediaTypeId], [Units], [StoreId], [IsAvaliable]) VALUES (12, 1005, 3, 5, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[FilmsInMedia] OFF
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
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO
SET IDENTITY_INSERT [dbo].[StoreLocations] ON 
GO
INSERT [dbo].[StoreLocations] ([Id], [Address], [Phone], [Email], [OpeningTime], [ClosingTime]) VALUES (1, N'ул. Ленина, д. 10', N'+7(495)111-22-33', N'store1@example.com', CAST(N'10:00:00' AS Time), CAST(N'20:00:00' AS Time))
GO
INSERT [dbo].[StoreLocations] ([Id], [Address], [Phone], [Email], [OpeningTime], [ClosingTime]) VALUES (2, N'ул. Пушкина, д. 5', N'+7(495)222-33-44', N'store2@example.com', CAST(N'09:00:00' AS Time), CAST(N'19:00:00' AS Time))
GO
INSERT [dbo].[StoreLocations] ([Id], [Address], [Phone], [Email], [OpeningTime], [ClosingTime]) VALUES (3, N'ул. Гоголя, д. 12', N'+7(495)333-44-55', N'store3@example.com', CAST(N'11:00:00' AS Time), CAST(N'21:00:00' AS Time))
GO
INSERT [dbo].[StoreLocations] ([Id], [Address], [Phone], [Email], [OpeningTime], [ClosingTime]) VALUES (4, N'ул. Толстого, д. 8', N'+7(495)444-55-66', N'store4@example.com', CAST(N'10:30:00' AS Time), CAST(N'20:30:00' AS Time))
GO
SET IDENTITY_INSERT [dbo].[StoreLocations] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (2, 1002, 1, CAST(N'2023-05-07T17:36:05.473' AS DateTime), CAST(N'2023-05-10T17:36:05.473' AS DateTime), 0, 3, 2000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (1002, 1002, 1, CAST(N'2023-05-07T17:43:05.943' AS DateTime), CAST(N'2023-05-13T17:43:05.943' AS DateTime), 0, 2, 4000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (2002, 1002, 1, CAST(N'2023-05-07T19:52:26.830' AS DateTime), CAST(N'2023-05-13T19:52:26.830' AS DateTime), 0, 1, 4000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (3002, 1002, 1, CAST(N'2023-05-08T11:05:14.933' AS DateTime), CAST(N'2023-05-11T11:05:14.933' AS DateTime), 0, 3, 2000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (3003, 1002, 1, CAST(N'2023-05-08T12:53:12.830' AS DateTime), CAST(N'2023-05-11T12:53:12.830' AS DateTime), 0, 2, 2000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (3004, 1002, 1, CAST(N'2023-05-08T13:25:20.673' AS DateTime), CAST(N'2023-05-11T13:25:20.673' AS DateTime), 0, 2, 2000.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (3005, 1003, 1, CAST(N'2023-05-08T13:25:40.480' AS DateTime), CAST(N'2023-05-11T13:25:40.480' AS DateTime), 0, 4, 2500.0000)
GO
INSERT [dbo].[Transactions] ([Id], [FilmId], [CustomerId], [StartDate], [EndDate], [IsIssuied], [VideosInMediaId], [TotalPrice]) VALUES (3006, 1003, 1007, CAST(N'2023-05-08T13:50:02.567' AS DateTime), CAST(N'2023-05-14T13:50:02.567' AS DateTime), 0, 4, 5000.0000)
GO
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (1, N'user1@example.com', N'password1')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (2, N'user2@example.com', N'password2')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (3, N'user3@example.com', N'password3')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (1006, N'ivan_Serg@gmail.com', N'ivan_Serg@gmail.com')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password]) VALUES (1007, N'ivan123@gmail.com', N'password1')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [UQ__VideosIn__51C93937FCAF3C89]    Script Date: 08.05.2023 14:56:02 ******/
ALTER TABLE [dbo].[FilmsInMedia] ADD  CONSTRAINT [UQ__VideosIn__51C93937FCAF3C89] UNIQUE NONCLUSTERED 
(
	[FilmId] ASC,
	[MediaTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT ((0)) FOR [InBlackList]
GO
ALTER TABLE [dbo].[FilmsInMedia] ADD  CONSTRAINT [DF_VideosInMedia_IsAvaliable]  DEFAULT ((1)) FOR [IsAvaliable]
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
USE [master]
GO
ALTER DATABASE [VideoRentalDB] SET  READ_WRITE 
GO
