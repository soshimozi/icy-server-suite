/****** Object:  StoredProcedure [dbo].[uspAddPlaylistEntry]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAddPlaylistEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspAddPlaylistEntry]
	-- Add the parameters for the stored procedure here
	@playlistId bigint,
	@trackId bigint,
	@playlistIndex int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.PlaylistSongs 
		(playlistId, songId, playlistIndex)
	VALUES
		(@playlistId, @trackId, @playlistIndex)
END


' 
END
GO
