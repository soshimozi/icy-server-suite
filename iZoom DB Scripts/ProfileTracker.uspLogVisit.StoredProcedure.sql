/****** Object:  StoredProcedure [ProfileTracker].[uspLogVisit]    Script Date: 09/25/2007 20:39:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ProfileTracker].[uspLogVisit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [ProfileTracker].[uspLogVisit]
	-- Add the parameters for the stored procedure here
	@userId uniqueidentifier,
	@address varchar(512),
	@country varchar(256),
	@city varchar(256),
	@longitude float,
	@latitude float,
	@time datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT 
	INTO ProfileTracker.ProfileVisitLog
		(userId, address, country, city, longitude, latitude, [time])
	VALUES	
		(@userId, @address, @country, @city, @longitude, @latitude, @time)
END


' 
END
GO
