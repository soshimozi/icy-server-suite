/****** Object:  Schema [ProfileTracker]    Script Date: 09/25/2007 20:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'ProfileTracker')
EXEC sys.sp_executesql N'CREATE SCHEMA [ProfileTracker] AUTHORIZATION [dbo]'
GO
