/****** Object:  StoredProcedure [dbo].[uspRemovePlaylistEntry]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspRemovePlaylistEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspRemovePlaylistEntry]
	-- Add the parameters for the stored procedure here
	@playlistId bigint,
	@trackId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM dbo.PlaylistSongs WHERE dbo.PlaylistSongs.playlistId = @playlistId And dbo.PlaylistSongs.songId = @trackId;
END

' 
END
GO
