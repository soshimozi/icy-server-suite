/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 09/25/2007 20:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]'
GO
