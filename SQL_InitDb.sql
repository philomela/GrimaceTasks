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

/*Posts table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'Posts')
CREATE TABLE Grimace.Posts (
	[Id] BIGINT PRIMARY KEY,
	[Points] INT,
	[Url] NVARCHAR(500) NOT NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[UpdatedAt] DATETIME2 NOT NULL,
	[Expires] DATETIME2 NOT NULL,
	[SocialNetworkId] BIGINT NOT NULL FOREIGN KEY REFERENCES Grimace.SocialNetworks(Id)
)
GO

/*Participants table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'Participants')
CREATE TABLE Grimace.Participants (
	[Id] BIGINT PRIMARY KEY,
	[UserName] NVARCHAR(300) NOT NULL,
	[ParentId] BIGINT NOT NULL,
	[SocialNetworkId] BIGINT NOT NULL FOREIGN KEY REFERENCES Grimace.SocialNetworks(Id)
)
GO

/*TasksResults table*/
IF NOT EXISTS (SELECT 1 FROM sys.all_objects WHERE [name] = 'CheckResults')
CREATE TABLE Grimace.CheckResults (
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