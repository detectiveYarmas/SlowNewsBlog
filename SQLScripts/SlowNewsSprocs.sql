USE SlowNewsBlog
GO
--drop sprocs
DROP PROCEDURE IF EXISTS dbo.AddNewHashTag
DROP PROCEDURE IF EXISTS dbo.AddNewBlogPost
DROP PROCEDURE IF EXISTS dbo.AddNewCatagory
DROP PROCEDURE IF EXISTS dbo.AddHashTagToBlogPost
DROP PROCEDURE IF EXISTS dbo.AddCatagoryToBlogPost
DROP PROCEDURE IF EXISTS dbo.GetBlog
DROP PROCEDURE IF EXISTS dbo.GetAllBlogs
DROP PROCEDURE IF EXISTS dbo.GetBlogsByHashTag
DROP PROCEDURE IF EXISTS dbo.GetBlogsByCatagory
DROP PROCEDURE IF EXISTS dbo.GetHashTag
DROP PROCEDURE IF EXISTS dbo.GetCatagory
DROP PROCEDURE IF EXISTS dbo.RemoveBlog
DROP PROCEDURE IF EXISTS dbo.RemoveHashTag
DROP PROCEDURE IF EXISTS dbo.RemoveBloggerFromBlogPost
DROP PROCEDURE IF EXISTS dbo.RemoveCatagory
DROP PROCEDURE IF EXISTS dbo.RemoveCatagoryFromBlogPost
DROP PROCEDURE IF EXISTS dbo.RemoveHashTagFromBlogPost
DROP PROCEDURE IF EXISTS dbo.ApproveBlog
DROP PROCEDURE IF EXISTS dbo.ApproveHashTag
DROP PROCEDURE IF EXISTS dbo.DisapproveBlog
DROP PROCEDURE IF EXISTS dbo.DisapproveHashTag
DROP PROCEDURE IF EXISTS dbo.UpdateHashTag
DROP PROCEDURE IF EXISTS dbo.UpdateBlogPost
DROP PROCEDURE IF EXISTS dbo.UpdateCatagory
DROP PROCEDURE IF EXISTS dbo.AddVoteToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.DeleteVotToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.GetAllHashTags
DROP PROCEDURE IF EXISTS dbo.GetAllApprovedBlogPosts
DROP PROCEDURE IF EXISTS dbo.GetAllDisapprovedBlogPosts
DROP PROCEDURE IF EXISTS dbo.DeleteVoteToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.GetAllCatagories
DROP PROCEDURE IF EXISTS dbo.GetAllApprovedHashTags
DROP PROCEDURE IF EXISTS dbo.GetAllUnapprovedHashTags
DROP PROCEDURE IF EXISTS dbo.GetNewestBlogs
DROP PROCEDURE IF EXISTS dbo.HashTagUpdate
DROP PROCEDURE IF EXISTS dbo.GetHashTagsForBlogPost
DROP PROCEDURE IF EXISTS dbo.GetAllBloggers
DROP PROCEDURE IF EXISTS dbo.GetBlogsByBlogger
DROP PROCEDURE IF EXISTS dbo.GetAllHashTags
DROP PROCEDURE IF EXISTS dbo.GetStaticPageById
DROP PROCEDURE IF EXISTS dbo.AddStaticPage
DROP PROCEDURE IF EXISTS dbo.DeleteStaticPage
DROP PROCEDURE IF EXISTS dbo.UpdateStaticPage
DROP PROCEDURE IF EXISTS dbo.GetStaticPages
GO


--static page sprocs
CREATE PROCEDURE DeleteStaticPage (@staticPageId INT)
AS
DELETE FROM StaticPages
WHERE StaticPageId = @staticPageId
GO

CREATE PROCEDURE UpdateStaticPage (@staticPageId INT, @title NVARCHAR(100) , @body TEXT)
AS
UPDATE StaticPages
SET Title = @title, Body = @body
WHERE StaticPageId = @staticPageId
GO

CREATE PROCEDURE AddStaticPage (@title NVARCHAR(100), @body TEXT, @staticPageId INT OUTPUT)
AS
INSERT INTO StaticPages (Title, Body)
VALUES (@title, @body)
SET @staticPageId = SCOPE_IDENTITY();
GO

-- add new sprocs
CREATE PROCEDURE GetAllBloggers
AS
BEGIN
	SELECT au.UserName
	FROM AspNetUsers au
	INNER JOIN AspNetUserRoles ar ON au.Id = ar.UserId
	INNER JOIN AspNetRoles ON AspNetRoles.Id = ar.RoleId
	WHERE ar.RoleId = (SELECT RoleId FROM AspNetUserRoles WHERE AspNetRoles.Name = 'Blogger')
END
GO

CREATE PROCEDURE AddNewHashTag (@hashTagId INT OUTPUT, @hashTagName NVARCHAR(50))
AS
BEGIN
INSERT INTO HashTags (HashTagName)
VALUES (@hashTagName)
SET @hashTagId = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE HashTagUpdate (@hashTagId INT, @hashTagName NVARCHAR(50), @Approved BIT)
AS
BEGIN 
	UPDATE HashTags SET
		HashTagName = @hashTagName,
		Approved = @Approved
	WHERE HashTagId = @hashTagId
END
GO

CREATE PROCEDURE AddNewBlogPost (@blog TEXT, @title NVARCHAR(100), @BlogPostId INT OUTPUT, @CatagoryId INT, 
@HeaderImage nvarchar(128), @Id nvarchar(128)) --when using this sproc, you must then add the blogger(AddAuthorToBlogPost) in a seperate call for the bridge table
AS
BEGIN
INSERT INTO BlogPosts (Blog, Title, CatagoryId, HeaderImage, Id)
VALUES (@blog, @title, @CatagoryId, @HeaderImage, @Id)
SET @BlogPostId = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE AddNewCatagory (@CatagoryName NVARCHAR(50), @catagoryId INT OUTPUT)
AS
BEGIN
INSERT INTO Catagories (CatagoryName)
VALUES (@CatagoryName)
SET @catagoryId = SCOPE_IDENTITY()
END
GO




-- get sprocs
CREATE PROCEDURE GetStaticPageById (@staticPageId INT)
AS
SELECT *
FROM StaticPages
WHERE StaticPageId = @staticPageId
GO

CREATE PROCEDURE GetStaticPages
AS
SELECT Title, StaticPageId, Body
FROM StaticPages
GO

CREATE PROCEDURE GetNewestBlogs
AS
SELECT TOP 10 *
FROM BlogPosts
WHERE Approved = 1 AND PublishDate IS NOT NULL AND GetDate() >= PublishDate
ORDER BY PublishDate DESC
GO

CREATE PROCEDURE GetBlogsByBlogger @Id nvarchar(256)
AS
SELECT *
FROM BlogPosts
WHERE BlogPosts.Id = (SELECT AspNetUsers.Id FROM AspNetUsers WHERE Email = @Id)
GO

CREATE PROCEDURE GetAllCatagories
AS
SELECT *
FROM Catagories
GO

CREATE PROCEDURE GetAllHashTags
AS
SELECT HashTagId, HashTagName, Approved
FROM HashTags
GO

CREATE PROCEDURE GetAllApprovedHashTags
AS
SELECT HashTagId, HashTagName, Approved
FROM HashTags
WHERE Approved = 1
GO

CREATE PROCEDURE GetAllUnapprovedHashTags
AS
SELECT HashTagId, HashTagName, Approved
FROM HashTags
WHERE Approved = 0
GO

CREATE PROCEDURE GetAllApprovedBlogPosts
AS
SELECT *
FROM BlogPosts
WHERE Approved = 1
GO

CREATE PROCEDURE GetAllDisapprovedBlogPosts
AS SELECT *
FROM BlogPosts
WHERE Approved = 0
GO

CREATE PROCEDURE GetBlogsByCatagory @catagoryId INT
AS
SELECT *
FROM BlogPosts
WHERE @catagoryId = BlogPosts.CatagoryId AND Approved = 1 AND GETDATE() >= PublishDate
GO

CREATE PROCEDURE GetBlog @blogId INT
AS
SELECT *
FROM BlogPosts
WHERE @blogId = BlogPostId
GO

CREATE PROCEDURE GetAllBlogs
AS
SELECT *
FROM BlogPosts
GO

CREATE PROCEDURE GetBlogsByHashTag @hashtagId INT
AS
SELECT *
FROM HashTags
INNER JOIN BlogPostsHashTags ON HashTags.HashTagId = BlogPostsHashTags.HashTagId
INNER JOIN BlogPosts ON BlogPosts.BlogPostId =  BlogPostsHashTags.BlogPostId
WHERE @hashtagId = HashTags.HashTagId AND BlogPosts.Approved = 1 AND GETDATE() >= PublishDate
GO

CREATE PROCEDURE GetHashTag @hashTagId INT
AS
SELECT *
FROM HashTags
WHERE HashTagId = @hashTagId
GO
CREATE PROCEDURE GetCatagory @catagoryId INT
AS
SELECT *
FROM Catagories
WHERE CatagoryId = @catagoryId
GO

CREATE PROCEDURE GetHashTagsForBlogPost @blogPostId INT
AS
SELECT *
FROM BlogPostsHashTags
INNER JOIN HashTags ON BlogPostsHashTags.HashTagId = HashTags.HashTagId
WHERE BlogPostId = @blogPostId AND HashTags.Approved = 1
GO


-- remove sprocs
CREATE PROCEDURE RemoveBlog @blogId INT
AS
BEGIN
DELETE FROM BlogPostsHashTags
WHERE @blogId = BlogPostId


DELETE FROM BlogPosts
WHERE @blogId = BlogPostId
END
GO

CREATE PROCEDURE RemoveHashTag @hashTagId INT
AS
BEGIN
DELETE FROM BlogPostsHashTags
WHERE @hashTagId = HashTagId

DELETE FROM HashTags
WHERE @hashTagId = HashTagId
END
GO

CREATE PROCEDURE RemoveBloggerFromBlogPost ( @blogPostId INT)
AS
UPDATE BlogPosts SET Id=null WHERE BlogPosts.BlogPostId=@blogPostId;

GO

CREATE PROCEDURE RemoveCatagory @catagoryId INT
AS
BEGIN
UPDATE BlogPosts SET CatagoryId=null WHERE BlogPosts.CatagoryId=@catagoryId;
DELETE FROM Catagories
WHERE @catagoryId = CatagoryId
End
go

CREATE PROCEDURE RemoveCatagoryFromBlogPost( @blogPostId INT)
AS
UPDATE BlogPosts SET CatagoryId=null WHERE BlogPosts.BlogPostId=@blogPostId;

GO

CREATE PROCEDURE RemoveHashTagFromBlogPost  @blogPostId INT
AS
DELETE FROM BlogPostsHashTags
WHERE @blogPostId = BlogPostId
GO


--approval sprocs
CREATE PROCEDURE ApproveBlog @blogPostId INT
AS
UPDATE BlogPosts
SET Approved = 1
WHERE BlogPostId = @blogPostId
GO

CREATE PROCEDURE ApproveHashTag @hashTagId INT
AS
UPDATE HashTags
SET  Approved = 1
WHERE HashTagId = @hashTagId
GO

CREATE PROCEDURE DisapproveBlog @blogPostId INT
AS
UPDATE BlogPosts
SET Approved = 0
WHERE BlogPostId = @blogPostId
GO

CREATE PROCEDURE DisapproveHashTag @hashTagId INT, @blogPostId INT
AS
UPDATE BlogPostsHashTags
SET Approved = 0
WHERE BlogPostId = @blogPostId AND HashTagId = @hashTagId
GO


--update sprocs
CREATE PROCEDURE UpdateHashtag @hashtagId INT, @hashTag NVARCHAR(50)
AS
UPDATE HashTags
SET HashTagName = @hashTag
WHERE HashTagId = @hashTagId
GO

CREATE PROCEDURE UpdateBlogPost (@blog TEXT, @title NVARCHAR(100), @BlogPostId INT, @CatagoryId INT, 
@HeaderImage NVARCHAR(128), @Approved bit)
AS
UPDATE BlogPosts
SET Blog = @blog, Title =@title, CatagoryId =  @CatagoryId, HeaderImage =@HeaderImage, Approved =@Approved
WHERE BlogPostId = @BlogPostId
GO



CREATE PROCEDURE UpdateCatagory @catagoryId INT, @catagoryName NVARCHAR(50)
AS
UPDATE Catagories
SET CatagoryName = @catagoryName
WHERE CatagoryId = @catagoryId
GO

--vote count change
CREATE PROCEDURE AddVoteToBlogPostHashTag @blogPostId INT, @hashTagId INT
AS
UPDATE BlogPostsHashTags
SET VoteCount = VoteCount + 1
WHERE BlogPostId = @blogPostId AND HashTagId = @hashTagId
GO

CREATE PROCEDURE DeleteVoteToBlogPostHashTag @blogPostId INT, @hashTagId INT
AS
UPDATE BlogPostsHashTags
SET VoteCount = VoteCount - 1
WHERE BlogPostId = @blogPostId AND HashTagId = @hashTagId
GO

--add to sprocs
CREATE PROCEDURE AddHashTagToBlogPost @hashTagId INT, @blogPostId INT
AS
INSERT INTO BlogPostsHashTags (HashTagId, BlogPostId)
VALUES ((SELECT HashTagId FROM HashTags WHERE HashTagId = @hashTagId), (SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = @blogPostId))
GO

CREATE PROCEDURE AddCatagoryToBlogPost @catagoryId INT, @blogPostId INT
AS
UPDATE BlogPosts SET CatagoryId=@catagoryId WHERE BlogPosts.BlogPostId=@blogPostId;
GO
