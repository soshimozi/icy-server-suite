/****** Object:  Role [aspnet_Roles_FullAccess]    Script Date: 09/25/2007 20:39:40 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_FullAccess]
GO
