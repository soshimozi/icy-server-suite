/****** Object:  StoredProcedure [MySpaceCodes].[uspAddContactTable]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MySpaceCodes].[uspAddContactTable]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [MySpaceCodes].[uspAddContactTable]
	@code varchar(2048),
	@imagetype varchar(256),
	@imagedata image
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT 
	INTO MySpaceCodes.ContactTable
		(code, imagedata, imagetype)
	VALUES
		(@code, @imagedata, @imagetype)

	SELECT SCOPE_IDENTITY();

END



' 
END
GO
