USE [ArenaDB]
GO

/****** Object:  Table [dbo].[cust_evnt_host_home]    Script Date: 12/04/2014 23:01:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cust_kfs_story]') AND type in (N'U'))
DROP TABLE [dbo].[cust_kfs_story]
GO

USE [ArenaDB]
GO

/****** Object:  Table [dbo].[cust_kfs_story]    Script Date: 12/04/2014 23:01:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[cust_kfs_story](
	[story_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[person_id] [int] NULL,
	[category_luid] [int] NULL,
	[organization_id] [int] NULL,
	[approved] [bit] NULL,
	[first_name] [varchar](100) NULL,
	[last_name] [varchar](100) NULL,	
	[email] [varchar](200) NULL,
	[approver] [int] NULL,
	[source_key] [varchar](200) NULL,
	[thumb_key] [varchar](200) NULL,
	[vimeo_key] [varchar](100) NULL,
	[allow_posting_online] [bit] NULL,
	[allow_promo] [bit] NULL,
	[date_created] [datetime] NULL,
	[public_url] [varchar](250) NULL,
	[created_by] [varchar](50) NULL,
	[date_modified] [datetime] NULL,
	[modified_by] [varchar](50) NULL
 CONSTRAINT [PK_cust_kfs_story] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[cust_kfs_story]  WITH CHECK ADD CONSTRAINT [FK_cust_kfs_story_core_person] FOREIGN KEY([person_id])
REFERENCES [dbo].[core_person] ([person_id])
GO

ALTER TABLE [dbo].[cust_kfs_story] CHECK CONSTRAINT [FK_cust_kfs_story_core_person]
GO

ALTER TABLE [dbo].[cust_kfs_story]  WITH CHECK ADD CONSTRAINT [FK_cust_kfs_story_core_approver] FOREIGN KEY([approver])
REFERENCES [dbo].[core_person] ([person_id])
GO

ALTER TABLE [dbo].[cust_kfs_story] CHECK CONSTRAINT [FK_cust_kfs_story_core_approver]
GO

/*ALTER TABLE [dbo].[cust_kfs_story]  WITH CHECK ADD CONSTRAINT [FK_cust_kfs_story_core_lookup] FOREIGN KEY([category_luid])
REFERENCES [dbo].[core_lookup] ([lookup_id])
GO*/

ALTER TABLE [dbo].[cust_kfs_story] WITH CHECK ADD CONSTRAINT [FK_cust_kfs_story_core_comp_lookup]
  FOREIGN KEY ([category_luid], [organization_id]) REFERENCES [dbo].[core_lookup] ([lookup_id], [organization_id])
GO

ALTER TABLE [dbo].[cust_kfs_story] CHECK CONSTRAINT [FK_cust_kfs_story_core_comp_lookup]
GO
