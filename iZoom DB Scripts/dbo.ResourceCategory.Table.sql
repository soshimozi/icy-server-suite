/****** Object:  Table [dbo].[ResourceCategory]    Script Date: 09/25/2007 20:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResourceCategory](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [varchar](1024) NULL,
 CONSTRAINT [PK_ResourceCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
