/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 09/25/2007 20:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_WebEvent_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]'
GO
