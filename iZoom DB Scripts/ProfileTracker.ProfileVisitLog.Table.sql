/****** Object:  Table [ProfileTracker].[ProfileVisitLog]    Script Date: 09/25/2007 20:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ProfileTracker].[ProfileVisitLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [ProfileTracker].[ProfileVisitLog](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userid] [uniqueidentifier] NULL,
	[address] [varchar](256) NULL,
	[country] [varchar](256) NULL,
	[city] [varchar](256) NULL,
	[longitude] [float] NULL,
	[latitude] [float] NULL,
	[time] [datetime] NULL,
 CONSTRAINT [PK_ProfileVisitLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
