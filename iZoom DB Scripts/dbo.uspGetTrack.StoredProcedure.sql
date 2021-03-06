/****** Object:  StoredProcedure [dbo].[uspGetTrack]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTrack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspGetTrack]
	-- Add the parameters for the stored procedure here
		@accountId uniqueidentifier,
		@songId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		description, url, id, AccountId, artist, title, album, genre, hash 
	FROM
		[vwTracks] 
	WHERE 
		([AccountId] = @accountId 
				Or 
		IsNull([AccountId], ''00000000-0000-0000-0000-000000000000'') = ''00000000-0000-0000-0000-000000000000'')	
			And
		id = @songId
		
END
' 
END
GO
