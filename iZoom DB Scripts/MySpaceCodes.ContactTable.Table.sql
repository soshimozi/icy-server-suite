/****** Object:  Table [MySpaceCodes].[ContactTable]    Script Date: 09/25/2007 20:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MySpaceCodes].[ContactTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [MySpaceCodes].[ContactTable](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[code] [varchar](2048) NULL,
	[imagetype] [varchar](256) NULL,
	[imagedata] [image] NULL,
 CONSTRAINT [PK_ContactTables] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
