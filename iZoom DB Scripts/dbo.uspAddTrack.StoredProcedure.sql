/****** Object:  StoredProcedure [dbo].[uspAddTrack]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAddTrack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspAddTrack]
	-- Add the parameters for the stored procedure here
	@accountId uniqueidentifier = Null,
	@description varchar(255),
	@url varchar(1024),
	@artist varchar(128) = '''',
	@title varchar(128) = '''',
	@album varchar(128) = '''',
	@genre int = Null,
	@storageId bigint = Null,
	@hash varchar(1024) = ''''

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.Track
		(accountId, description, url, artist, title, album, genre, hash, storageId)
	VALUES
		(@accountId, @description, @url, @artist, @title, @album, @genre, @hash, @storageId)

	SELECT SCOPE_IDENTITY();
END





' 
END
GO
