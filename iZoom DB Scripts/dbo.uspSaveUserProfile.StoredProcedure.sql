/****** Object:  StoredProcedure [dbo].[uspSaveUserProfile]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveUserProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSaveUserProfile]
	-- Add the parameters for the stored procedure here
	@userId uniqueidentifier,
	@autoPlay bit,
	@random bit,
	@continue bit,
	@playlistpublic bit,
	@userpublic bit,
	@trackspublic bit,
	@volume int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT userid FROM dbo.AccountConfig WHERE userid = @userId;
	
	IF @@ROWCOUNT = 0
		BEGIN
			INSERT INTO dbo.AccountConfig
				(random, autoplay, [continue], userid, volume, playlistpublic, userpublic, trackspublic)
			VALUES
				(@random, @autoPlay, @continue, @userId, @volume, @playlistpublic, @userpublic, @trackspublic);
		END
	ELSE
		BEGIN
			UPDATE dbo.AccountConfig
			SET random = @random, 
				autoplay = @autoPlay, 
				[continue] = @continue,
				volume = @volume,
				playlistpublic = @playlistpublic,
				userpublic = @userpublic,
				trackspublic = @trackspublic
			WHERE
				userid = @userId;
		END
END



' 
END
GO
