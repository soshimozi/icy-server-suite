/****** Object:  StoredProcedure [dbo].[uspAddUserTrack]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAddUserTrack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspAddUserTrack]
	-- Add the parameters for the stored procedure here
	@userId uniqueidentifier,
	@size bigint,
	@description varchar(1024),
	@data image
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO 
		dbo.UserStorage
		(userId, [size], data, description)
	VALUES	
		(@userId, @size, @data, @description);

	SELECT SCOPE_IDENTITY();

END



' 
END
GO
