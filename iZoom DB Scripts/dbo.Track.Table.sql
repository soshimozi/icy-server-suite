/****** Object:  Table [dbo].[Track]    Script Date: 09/25/2007 20:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Track]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Track](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountId] [uniqueidentifier] NULL,
	[url] [varchar](1024) NULL,
	[description] [varchar](1024) NULL,
	[artist] [varchar](512) NULL,
	[title] [varchar](1024) NULL,
	[album] [varchar](512) NULL,
	[genre] [int] NULL,
	[hash] [varchar](1024) NULL,
	[storageId] [bigint] NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
