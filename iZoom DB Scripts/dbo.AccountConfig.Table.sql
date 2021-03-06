/****** Object:  Table [dbo].[AccountConfig]    Script Date: 09/25/2007 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountConfig]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountConfig](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[random] [bit] NULL,
	[autoplay] [bit] NULL,
	[continue] [bit] NULL,
	[volume] [int] NULL,
	[playlistpublic] [bit] NULL,
	[userpublic] [bit] NULL,
	[trackspublic] [bit] NULL,
	[playlistId] [bigint] NULL,
	[userid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Config_Playlist]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountConfig]'))
ALTER TABLE [dbo].[AccountConfig]  WITH CHECK ADD  CONSTRAINT [FK_Config_Playlist] FOREIGN KEY([playlistId])
REFERENCES [dbo].[Playlist] ([id])
GO
ALTER TABLE [dbo].[AccountConfig] CHECK CONSTRAINT [FK_Config_Playlist]
GO
