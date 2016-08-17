/****** Object:  StoredProcedure [dbo].[uspUpdatePlaylistEntry]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdatePlaylistEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdatePlaylistEntry]
	@playlistId bigint,
	@songId	bigint,
	@newPlaylistIndex int


AS
BEGIN
		UPDATE
			dbo.PlaylistSongs
		SET
			playlistIndex = @newPlaylistIndex
		WHERE
			playlistId = @playlistId And songId = @songId

END
' 
END
GO
