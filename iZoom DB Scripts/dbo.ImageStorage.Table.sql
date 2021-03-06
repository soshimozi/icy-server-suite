/****** Object:  Table [dbo].[ImageStorage]    Script Date: 09/25/2007 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageStorage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImageStorage](
	[id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ImageStorage_id]  DEFAULT (newid()),
	[ImageType] [varchar](256) NOT NULL,
	[ImageData] [image] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ImageStorage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
