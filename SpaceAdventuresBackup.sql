USE [master]
GO
/****** Object:  Database [SpaceAdventures]    Script Date: 15-06-22 20:42:23 ******/
CREATE DATABASE [SpaceAdventures]
 CONTAINMENT = NONE
GO
ALTER DATABASE [SpaceAdventures] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SpaceAdventures].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ARITHABORT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SpaceAdventures] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SpaceAdventures] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SpaceAdventures] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SpaceAdventures] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SpaceAdventures] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SpaceAdventures] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SpaceAdventures] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SpaceAdventures] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SpaceAdventures] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SpaceAdventures] SET RECOVERY FULL 
GO
ALTER DATABASE [SpaceAdventures] SET  MULTI_USER 
GO
ALTER DATABASE [SpaceAdventures] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SpaceAdventures] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SpaceAdventures] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SpaceAdventures] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SpaceAdventures] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SpaceAdventures] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SpaceAdventures', N'ON'
GO
ALTER DATABASE [SpaceAdventures] SET QUERY_STORE = ON
GO
ALTER DATABASE [SpaceAdventures] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SpaceAdventures]
GO
/****** Object:  Table [dbo].[Aircraft]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aircraft](
	[IdAircraft] [int] IDENTITY(1,1) NOT NULL,
	[Manufacturer] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[NumberOfSeats] [int] NOT NULL,
 CONSTRAINT [PK_Aircraft] PRIMARY KEY CLUSTERED 
(
	[IdAircraft] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[IdAirport] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IdPlanet] [int] NOT NULL,
 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
(
	[IdAirport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[IdBooking] [int] IDENTITY(1,1) NOT NULL,
	[IdFlight] [int] NOT NULL,
	[IdClient] [int] NOT NULL,
	[NbSeats] [int] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[IdBooking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[IdUser] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[IdFlight] [int] IDENTITY(1,1) NOT NULL,
	[FlightStatus] [int] NOT NULL,
	[IdAircraft] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[IdItinerary] [int] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
	[RemainingSeats] [int] NOT NULL,
 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
(
	[IdFlight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Itinerary]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Itinerary](
	[IdItinerary] [int] IDENTITY(1,1) NOT NULL,
	[IdAirport1] [int] NOT NULL,
	[IdAirport2] [int] NOT NULL,
 CONSTRAINT [PK_Itinerary] PRIMARY KEY CLUSTERED 
(
	[IdItinerary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planet]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planet](
	[IdPlanet] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Planet] PRIMARY KEY CLUSTERED 
(
	[IdPlanet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15-06-22 20:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[VerifiedEmail] [bit] NOT NULL,
	[IdRole] [int] NOT NULL,
	[IdUserAuth0] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Aircraft] ON 

INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (2, N'Boeing', N'Fixed Wing 747 (Sofia)', 20)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (3, N'Boeing', N'Fixed Wing 757', 30)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (4, N'Boeing', N'B-52B', 25)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (5, N'Boieng', N'CH-47B Chinook - Rotorcraft', 35)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (6, N'Boieng', N'X-40', 15)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (8, N'Boeing', N'Dream Chaser', 40)
INSERT [dbo].[Aircraft] ([IdAircraft], [Manufacturer], [Model], [NumberOfSeats]) VALUES (9, N'Airbus', N'SpacePlane', 30)
SET IDENTITY_INSERT [dbo].[Aircraft] OFF
GO
SET IDENTITY_INSERT [dbo].[Airport] ON 

INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (1, N' Kennedy Space Center', 3)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (2, N'Edwards Air Force Base', 3)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (3, N'White Sands', 3)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (4, N'Ceres Landing Site', 9)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (5, N'Eris Landing Site', 13)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (6, N'Haumea Landing Site', 11)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (7, N'Jupiter Landing Site', 5)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (8, N'Makemake Landing Site', 12)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (9, N'Mars Landing Site', 4)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (10, N'Mercury Landing Site', 1)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (11, N'Moon Landing Site', 14)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (12, N'Neptune Landing Site', 8)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (13, N'Pluto Landing Site', 10)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (14, N'Saturn Landing Site', 6)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (15, N'Uranus Landing Site', 7)
INSERT [dbo].[Airport] ([IdAirport], [Name], [IdPlanet]) VALUES (16, N'Venus Landing Site', 2)
SET IDENTITY_INSERT [dbo].[Airport] OFF
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (2, 2, 6, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (3, 2, 9, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (4, 6, 9, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (5, 4, 7, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (6, 4, 7, 3)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (7, 5, 10, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (8, 6, 11, 2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (9, 4, 11, 3)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (10, 5, 11, 1)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (11, 4, 12, -2)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (12, 3, 12, 1)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (13, 6, 12, 3)
INSERT [dbo].[Booking] ([IdBooking], [IdFlight], [IdClient], [NbSeats]) VALUES (14, 7, 12, 10)
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (6, N'Test', N'Test', N'test@test.test', 29)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (7, N'Hammadi', N'Chadli', N'madi-owner@spaceadventures.be', 26)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (8, N'Fripon', N'HAllet', N'hallet.owner@spaceadventure.be', 39)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (9, N'Test', N'Dummy', N'mister.test@test.test', 43)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (10, N'Hammadi', N'Chadli', N'madi-customer@spaceadventures.be', 48)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (11, N'Test', N'User', N'user.test@test.test', 50)
INSERT [dbo].[Client] ([IdClient], [FirstName], [LastName], [Email], [IdUser]) VALUES (12, N'Jean', N'Valjean', N'jean.valjean@test.test', 51)
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Flight] ON 

INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (2, 0, 2, 1000, 8, CAST(N'2022-06-01T00:00:00.000' AS DateTime), CAST(N'2023-01-01T00:00:00.000' AS DateTime), 30)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (3, 1, 3, 1000, 8, CAST(N'2022-07-07T00:00:00.000' AS DateTime), CAST(N'2023-01-01T00:00:00.000' AS DateTime), 26)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (4, 1, 4, 1000, 8, CAST(N'2022-08-01T00:00:00.000' AS DateTime), CAST(N'2023-06-01T00:00:00.000' AS DateTime), 26)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (5, 1, 5, 1000, 8, CAST(N'2022-09-01T00:00:00.000' AS DateTime), CAST(N'2023-03-01T00:00:00.000' AS DateTime), 24)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (6, 1, 6, 3000, 1, CAST(N'2022-10-15T00:00:00.000' AS DateTime), CAST(N'2024-01-01T00:00:00.000' AS DateTime), 13)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (7, 1, 2, 4500, 3, CAST(N'2023-05-01T00:00:00.000' AS DateTime), CAST(N'2024-12-12T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (12, 1, 6, 2500, 2, CAST(N'2023-02-01T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), 20)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (14, 1, 8, 8700, 12, CAST(N'2025-03-14T00:00:00.000' AS DateTime), CAST(N'2026-05-05T00:00:00.000' AS DateTime), 30)
INSERT [dbo].[Flight] ([IdFlight], [FlightStatus], [IdAircraft], [Price], [IdItinerary], [DepartureTime], [ArrivalTime], [RemainingSeats]) VALUES (15, 1, 9, 2800, 11, CAST(N'2022-11-20T00:00:00.000' AS DateTime), CAST(N'2023-08-14T00:00:00.000' AS DateTime), 15)
SET IDENTITY_INSERT [dbo].[Flight] OFF
GO
SET IDENTITY_INSERT [dbo].[Itinerary] ON 

INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (1, 1, 7)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (2, 1, 11)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (5, 1, 13)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (4, 1, 16)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (11, 2, 6)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (12, 2, 10)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (10, 2, 11)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (3, 3, 9)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (8, 9, 3)
INSERT [dbo].[Itinerary] ([IdItinerary], [IdAirport1], [IdAirport2]) VALUES (9, 11, 13)
SET IDENTITY_INSERT [dbo].[Itinerary] OFF
GO
SET IDENTITY_INSERT [dbo].[Planet] ON 

INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (9, N'Ceres')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (3, N'Earth')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (13, N'Eris')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (11, N'Haumea')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (5, N'Jupiter')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (12, N'Makemake')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (4, N'Mars')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (1, N'Mercury')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (14, N'Moon')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (8, N'Neptune')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (10, N'Pluto')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (6, N'Saturn')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (7, N'Uranus')
INSERT [dbo].[Planet] ([IdPlanet], [Name]) VALUES (2, N'Venus')
SET IDENTITY_INSERT [dbo].[Planet] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([IdRole], [Name], [Description]) VALUES (1, N'Admin', N'R/W Messages - R Users')
INSERT [dbo].[Role] ([IdRole], [Name], [Description]) VALUES (2, N'Agent', N'R Messages')
INSERT [dbo].[Role] ([IdRole], [Name], [Description]) VALUES (3, N'Owner', N'R/W Messages - R/W Users')
INSERT [dbo].[Role] ([IdRole], [Name], [Description]) VALUES (4, N'User', N'R/W Messages')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (26, N'Madi-Owner', N'madi-owner@spaceadventures.be', 0, 3, N'auth0|629f1bc083b1d6006fce3542')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (27, N'Madi-Admin', N'madi-admin@spaceadventures.be', 0, 1, N'auth0|629f1c7b4925f70068973381')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (28, N'Madi-Agent', N'madi-agent@spaceadventures.be', 0, 2, N'auth0|629f1d6d6f5e8b0069e062ad')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (29, N'Madi-User', N'madi-user@spaceadventures.be', 0, 4, N'auth0|629f1d976f7f590069230918')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (39, N'Hallet-Owner', N'hallet.owner@spaceadventure.be', 0, 3, N'auth0|62a8515ff11851539180308e')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (40, N'Hallet-Agent', N'hallet.agent@spaceadventure.be', 0, 2, N'auth0|62a85192a51718dbb979873c')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (41, N'Hallet-Admin', N'hallet.admin@spaceadventure.be', 0, 1, N'auth0|62a851b7a1b9aac661f7e044')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (42, N'Hallet-User', N'hallet.user@spaceadventure.be', 0, 4, N'auth0|62a851ea1fd72bf68ac3ee1f')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (43, N'banane', N'psr11307@students.ephec.be', 0, 4, N'auth0|62a85f30dc7486a3ede157fb')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (48, N'Madi-Customer', N'madi-customer@spaceadventures.be', 0, 4, N'auth0|62a9b4f79e18332e5482e39c')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (49, N'Nobody', N'mister.anonymous@test.test', 0, 2, N'auth0|62a9e27957120c830fd153cd')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (50, N'TestUser', N'user.test@test.test', 0, 4, N'auth0|62a9e44e57120c830fd15445')
INSERT [dbo].[User] ([IdUser], [Username], [Email], [VerifiedEmail], [IdRole], [IdUserAuth0]) VALUES (51, N'Mousquetaire', N'jean.valjean@test.test', 0, 4, N'auth0|62a9fc4957120c830fd15a2d')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Client_Email]    Script Date: 15-06-22 20:42:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Client_Email] ON [dbo].[Client]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Itinerary_Aiport1_Aiport2]    Script Date: 15-06-22 20:42:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Itinerary_Aiport1_Aiport2] ON [dbo].[Itinerary]
(
	[IdAirport1] ASC,
	[IdAirport2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Planet_Name]    Script Date: 15-06-22 20:42:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Planet_Name] ON [dbo].[Planet]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Airport]  WITH CHECK ADD  CONSTRAINT [FK_Airport_Planet] FOREIGN KEY([IdPlanet])
REFERENCES [dbo].[Planet] ([IdPlanet])
GO
ALTER TABLE [dbo].[Airport] CHECK CONSTRAINT [FK_Airport_Planet]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([IdClient])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Client]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Flight] FOREIGN KEY([IdFlight])
REFERENCES [dbo].[Flight] ([IdFlight])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Flight]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Aircraft] FOREIGN KEY([IdAircraft])
REFERENCES [dbo].[Aircraft] ([IdAircraft])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Aircraft]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Itinerary] FOREIGN KEY([IdItinerary])
REFERENCES [dbo].[Itinerary] ([IdItinerary])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Itinerary]
GO
ALTER TABLE [dbo].[Itinerary]  WITH CHECK ADD  CONSTRAINT [FK_Itinerary_Airport1] FOREIGN KEY([IdAirport1])
REFERENCES [dbo].[Airport] ([IdAirport])
GO
ALTER TABLE [dbo].[Itinerary] CHECK CONSTRAINT [FK_Itinerary_Airport1]
GO
ALTER TABLE [dbo].[Itinerary]  WITH CHECK ADD  CONSTRAINT [FK_Itinerary_Airport2] FOREIGN KEY([IdAirport2])
REFERENCES [dbo].[Airport] ([IdAirport])
GO
ALTER TABLE [dbo].[Itinerary] CHECK CONSTRAINT [FK_Itinerary_Airport2]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [SpaceAdventures] SET  READ_WRITE 
GO
