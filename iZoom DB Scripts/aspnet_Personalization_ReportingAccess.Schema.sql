/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 09/25/2007 20:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]'
GO
