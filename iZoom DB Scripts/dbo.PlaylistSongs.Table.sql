/****** Object:  Table [dbo].[PlaylistSongs]    Script Date: 09/25/2007 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlaylistSongs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlaylistSongs](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[playlistId] [bigint] NULL,
	[songId] [bigint] NULL,
	[playlistIndex] [int] NOT NULL CONSTRAINT [DF_PlaylistSongs_playlistIndex]  DEFAULT ((0)),
 CONSTRAINT [PK_Songs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Playlist_Track1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlaylistSongs]'))
ALTER TABLE [dbo].[PlaylistSongs]  WITH CHECK ADD  CONSTRAINT [FK_Playlist_Track1] FOREIGN KEY([songId])
REFERENCES [dbo].[Track] ([id])
GO
ALTER TABLE [dbo].[PlaylistSongs] CHECK CONSTRAINT [FK_Playlist_Track1]
GO
