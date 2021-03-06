/****** Object:  StoredProcedure [dbo].[uspCreatePlaylist]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreatePlaylist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspCreatePlaylist]
	-- Add the parameters for the stored procedure here
	@accountId uniqueidentifier,
	@name varchar(128) = ''''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @newId bigint;

	INSERT 
	INTO dbo.Playlist
		([name])
	VALUES
		(@name);

	SET @newId = @@IDENTITY;

    -- Insert statements for procedure here
	UPDATE dbo.AccountConfig
	SET 
		playlistId = @newId
	WHERE 
		userid = @accountId;

END
' 
END
GO
