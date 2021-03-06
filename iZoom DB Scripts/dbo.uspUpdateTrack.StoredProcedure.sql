/****** Object:  StoredProcedure [dbo].[uspUpdateTrack]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateTrack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateTrack]
	-- Add the parameters for the stored procedure here
	@songId bigint,
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
	UPDATE dbo.Track
	SET accountId = @accountId, description = @description, url = @url, artist = @artist, title = @title, album = @album, genre = @genre, hash = @hash, storageId = @storageId
	WHERE id = @songId;

END
' 
END
GO
