SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON

/*Create Db*/
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'GrimaceTasks')
CREATE DATABASE GrimaceTasks;
GO

/*Connect Db*/
IF EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'GrimaceTasks')
USE GrimaceTasks;
GO

/*Create login and user*/
IF NOT EXISTS (SELECT 1 FROM sys.sql_logins WHERE [name] = 'GrimaceTasks')
CREATE LOGIN [Grimace] 
  WITH Password = N'Pa$$word',
  DEFAULT_DATABASE  = [GrimaceTasks]
  CREATE USER [Grimace] FOR LOGIN [Grimace] WITH DEFAULT_SCHEMA=[Grimace]
  ALTER ROLE [db_owner] ADD MEMBER [Grimace];
GO

/*Create schema in db for system tables*/
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE [name] = 'Grimace')
BEGIN
	EXEC('CREATE SCHEMA Grimace');
END
GO

/*SocialNetworks table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'SocialNetworks')
CREATE TABLE Grimace.SocialNetworks (
	[Id] TINYINT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL
)
GO

/*Tasks table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'Tasks')
CREATE TABLE Grimace.Tasks (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Points] INT,
	[Url] NVARCHAR(500) NOT NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[ExpireDate] DATETIME2 NOT NULL,
	[SocialNetworkId] BIGINT UNIQUE NOT NULL FOREIGN KEY REFERENCES Grimace.SocialNetworks(Id)
)
GO

/*Participants table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'Participants')
CREATE TABLE Grimace.Participants (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[UserName] NVARCHAR(300) NOT NULL,
	[SocialNetworkId] BIGINT UNIQUE NOT NULL FOREIGN KEY REFERENCES Grimace.SocialNetworks(Id)
)
GO

/*TasksResults table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'TasksResults')
CREATE TABLE Grimace.TasksResults (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[UserId] BIGINT,
	[TaskId] BIGINT,
	[Points] INT,
	[DateChecks] DATETIME2
)
GO

/*Create schema in db for system tables*/
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE [name] = 'Infra')
BEGIN
	EXEC('CREATE SCHEMA Infra');
END
GO

/*ApiLogs table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'ApiLogs')
CREATE TABLE Infra.ApiLogs (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Request] NVARCHAR(MAX),
	[Response] NVARCHAR(MAX),
	[MethodType] VARCHAR(10),
	[NameMethod] VARCHAR(20),
	[CreateDate] DATETIME2
)
GO