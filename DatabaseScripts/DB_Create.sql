USE [master]
GO

/****** Object:  Database [Invoices]    Script Date: 12/09/2021 8:45:33 PM ******/
DROP DATABASE [Invoices]
GO

/****** Object:  Database [Invoices]    Script Date: 12/09/2021 8:45:33 PM ******/
CREATE DATABASE [Invoices]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Invoices', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Invoices.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Invoices_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Invoices_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Invoices].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Invoices] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Invoices] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Invoices] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Invoices] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Invoices] SET ARITHABORT OFF 
GO

ALTER DATABASE [Invoices] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Invoices] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Invoices] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Invoices] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Invoices] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Invoices] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Invoices] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Invoices] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Invoices] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Invoices] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Invoices] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Invoices] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Invoices] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Invoices] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Invoices] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Invoices] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Invoices] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Invoices] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Invoices] SET  MULTI_USER 
GO

ALTER DATABASE [Invoices] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Invoices] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Invoices] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Invoices] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Invoices] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Invoices] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [Invoices] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Invoices] SET  READ_WRITE 
GO

