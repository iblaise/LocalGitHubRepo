USE [ArenaDB]
GO


SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[cust_kfs_sp_get_storyById]
@story_id int

AS

SELECT *
FROM cust_kfs_story
WHERE story_id = @story_id

GO


CREATE proc [dbo].[cust_kfs_sp_del_story]
@story_id int

AS

DELETE cust_kfs_story 
WHERE story_id = @story_id

GO



CREATE proc [dbo].[cust_kfs_sp_save_story]
	@story_id int,
	@title [varchar](200),
	@person_id [int] ,
	@category_luid [int] ,
	@organization_id [int] ,
	@approved [bit] ,
	@first_name [varchar](100) ,
	@last_name [varchar](100) ,	
	@email [varchar](200) ,
	@approver [int] ,
	@source_key [varchar](200) ,
	@source_folder [varchar](200) ,
	@thumb_key [varchar](200) ,
	@thumb_folder [varchar](200) ,
	@vimeo_key [varchar](100) ,
	@allow_posting_online [bit] ,
	@allow_promo [bit] ,
	@public_url [varchar](250) ,
	@date_created [datetime] ,
	@created_by [varchar](50) ,
	@date_modified [datetime] ,
	@modified_by [varchar](50),
	@ID int OUTPUT

AS

--upsert logic begins
IF @story_id = -1
BEGIN
--if @story_id doesn't exist, do insert
	INSERT INTO cust_kfs_story
	(
		title ,
		person_id ,
		category_luid ,
		organization_id ,
		approved ,
		first_name ,
		last_name ,	
		email ,
		approver ,
		source_key ,
		source_folder ,
		thumb_key ,
		thumb_folder ,
		vimeo_key ,
		allow_posting_online ,
		allow_promo ,
		date_created ,
		public_url ,
		created_by ,
		date_modified ,
		modified_by
	)
	VALUES
	(
		@title ,
		@person_id ,
		@category_luid ,
		@organization_id ,
		@approved ,
		@first_name ,
		@last_name ,	
		@email ,
		@approver ,
		@source_key ,
		@source_folder ,
		@thumb_key ,
		@thumb_folder ,
		@vimeo_key ,
		@allow_posting_online ,
		@allow_promo ,
		@date_created ,
		@public_url ,
		@created_by ,
		@date_modified ,
		@modified_by
	)
	SELECT @ID = SCOPE_IDENTITY()
END
ELSE
BEGIN
--otherwise, do update
	UPDATE dbo.cust_kfs_story
		SET title = @title,
		person_id = @person_id,
		category_luid = @category_luid,
		organization_id = @organization_id,
		approved = @approved,
		first_name = @first_name,
		last_name = @last_name,	
		email = @email,
		approver = @approver,
		source_key = @source_key,
		source_folder = @source_folder,
		thumb_key = @thumb_key,
		thumb_folder = @thumb_folder,
		vimeo_key = @vimeo_key,
		allow_posting_online = @allow_posting_online,
		allow_promo = @allow_promo,
		date_created = @date_created,
		public_url = @public_url,
		created_by = @created_by,
		date_modified = @date_modified,
		modified_by = @modified_by
		WHERE story_id = @story_id
	SELECT @ID = @story_id
END

GO

CREATE proc [dbo].[cust_kfs_sp_get_story_list]
@search_param varchar (100)

AS 

SELECT * FROM cust_kfs_story
WHERE (@search_param = ''
OR Title like '%' + @search_param + '%' 
OR first_name like '%' + @search_param + '%' 
OR last_name like '%' + @search_param + '%' 
OR email like '%' + @search_param + '%')
 

GO 


---******************Sproc Tests below...
--insert
declare @date datetime
select @date = GETDATE()
declare @ID int

exec [cust_kfs_sp_save_story] -1, 'TITLE', null, 10191, 1, 0, 'FName', 'LName', 'email@email.com', 4, 'Source Key',
'SourceFolder', 'thumb key', 'thumbfolder', 'vimeokey', 1, 1, @date, 'http://publicurl.com', 
'CreatedBy', @date, 'ModifiedBy', @ID

select @ID

--update
declare @date datetime
select @date = GETDATE()
declare @ID int

exec [cust_kfs_sp_save_story] 1, 'TITLE5', null, 10191, 1, 0, 'FName', 'LName', 'email@email.com', 4, 'Source Key',
'SourceFolder', 'thumb key', 'thumbfolder', 'vimeokey', 1, 1, @date, 'http://publicurl.com', 
'CreatedBy', @date, 'ModifiedBy', @ID

select @ID


--delete
exec cust_kfs_sp_del_story 3

--select by Id
exec cust_kfs_sp_get_storyById 1, 1

--select list with search
exec cust_kfs_sp_get_story_list ''

--select List of stories


select * from cust_kfs_story

select top 100 person_id from core_person
select top 100 lookup_id, organization_id from core_lookup