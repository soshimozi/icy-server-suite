/****** Object:  StoredProcedure [dbo].[uspSearchAccountTracklist]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAccountTracklist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSearchAccountTracklist]
	-- Add the parameters for the stored procedure here
		@userId uniqueidentifier,
		@filter varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		id, description, userId
	FROM
		dbo.UserStorage
	WHERE 
		[userId] = @userId  And description LIKE ''%'' + @filter + ''%''
END

' 
END
GO
