USE SlowNewsBlog
GO
--drop sprocs
DROP PROCEDURE IF EXISTS dbo.AddNewHashTag
DROP PROCEDURE IF EXISTS dbo.AddNewBlogPost
DROP PROCEDURE IF EXISTS dbo.AddNewBlogger
DROP PROCEDURE IF EXISTS dbo.AddNewCatagory
DROP PROCEDURE IF EXISTS dbo.AddHashTagToBlogPost
DROP PROCEDURE IF EXISTS dbo.AddCatagoryToBlogPost
DROP PROCEDURE IF EXISTS dbo.AddBloggerToBlogPost
DROP PROCEDURE IF EXISTS dbo.GetBlog
DROP PROCEDURE IF EXISTS dbo.GetAllBlogs
DROP PROCEDURE IF EXISTS dbo.GetBlogsByHashTag
DROP PROCEDURE IF EXISTS dbo.GetBlogsByCatagory
DROP PROCEDURE IF EXISTS dbo.GetHashTag
DROP PROCEDURE IF EXISTS dbo.GetCatagory
DROP PROCEDURE IF EXISTS dbo.RemoveBlog
DROP PROCEDURE IF EXISTS dbo.RemoveHashTag
DROP PROCEDURE IF EXISTS dbo.RemoveBloggerFromBlogPost
DROP PROCEDURE IF EXISTS dbo.RemoveBlogger
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
DROP PROCEDURE IF EXISTS dbo.UpdateBlogger
DROP PROCEDURE IF EXISTS dbo.AddVoteToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.DeleteVotToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.GetAllHashTags
DROP PROCEDURE IF EXISTS dbo.GetAllApprovedBlogPosts
DROP PROCEDURE IF EXISTS dbo.GetAllDisapprovedBlogPosts
DROP PROCEDURE IF EXISTS dbo.DeleteVoteToBlogPostHashTag
DROP PROCEDURE IF EXISTS dbo.GetAllCatagories
DROP PROCEDURE IF EXISTS dbo.GetAllApprovedHashTags
DROP PROCEDURE IF EXISTS dbo.GetAllUnapprovedHashTags
DROP PROCEDURE IF EXISTS dbo.GetBlogsByBlogger
DROP PROCEDURE IF EXISTS dbo.GetNewestBlogs
DROP PROCEDURE IF EXISTS dbo.HashTagUpdate
DROP PROCEDURE IF EXISTS dbo.GetHashTagsForBlogPost
GO


-- add new sprocs
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

CREATE PROCEDURE AddNewBlogPost (@blog TEXT, @title NVARCHAR(100), @blogPostId INT OUTPUT) --when using this sproc, you must then add the blogger(AddAuthorToBlogPost) in a seperate call for the bridge table
AS
BEGIN
INSERT INTO BlogPosts (Blog, Title)
VALUES (@blog, @title)
SET @blogPostId = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE AddNewBlogger (@BloggerName NVARCHAR(100), @bloggerId INT OUTPUT)
AS
BEGIN
INSERT INTO Bloggers (BloggerName)
VALUES (@BloggerName)
SET @bloggerId = SCOPE_IDENTITY()
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
CREATE PROCEDURE GetNewestBlogs
AS
SELECT TOP 10 *
FROM BlogPosts
ORDER BY PublishDate DESC
GO

CREATE PROCEDURE GetBlogsByBlogger @bloggerId INT
AS
SELECT *
FROM BlogPosts
INNER JOIN BlogPostsBloggers ON BlogPosts.BlogPostId = BlogPostsBloggers.BlogPostId
WHERE BloggerId = @bloggerId
GO

CREATE PROCEDURE GetAllCatagories
AS
SELECT *
FROM Catagories
GO

CREATE PROCEDURE GetAllHashTags
AS
SELECT *
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
INNER JOIN BlogPostsCatagories ON BlogPosts.BlogPostId = BlogPostsCatagories.BlogPostId
INNER JOIN Catagories ON BlogPostsCatagories.CatagoryId = Catagories.CatagoryId
WHERE @catagoryId = Catagories.CatagoryId
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
WHERE @hashtagId = HashTags.HashTagId
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
WHERE BlogPostId = @blogPostId
GO


-- remove sprocs
CREATE PROCEDURE RemoveBlog @blogId INT
AS
BEGIN
DELETE FROM BlogPostsHashTags
WHERE @blogId = BlogPostId

DELETE FROM BlogPostsBloggers
WHERE @blogId = BlogPostId

DELETE FROM BlogPostsCatagories
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

CREATE PROCEDURE RemoveBloggerFromBlogPost @bloggerId INT, @blogPostId INT
AS
DELETE FROM BlogPostsBloggers
WHERE @bloggerId = BloggerId AND @blogPostId = BlogPostId
GO

CREATE PROCEDURE RemoveBlogger @bloggerId INT
AS
BEGIN
DELETE FROM BlogPostsBloggers
WHERE BloggerId = @bloggerId

DELETE FROM Bloggers
WHERE BloggerId = @bloggerId
END
GO

CREATE PROCEDURE RemoveCatagory @catagoryId INT
AS
BEGIN
DELETE FROM BlogPostsCatagories
WHERE @catagoryId = CatagoryId

DELETE FROM Catagories
WHERE @catagoryId = CatagoryId
END
GO

CREATE PROCEDURE RemoveCatagoryFromBlogPost @catagoryId INT, @blogPostId INT
AS
DELETE FROM BlogPostsCatagories
WHERE @catagoryId = CatagoryId AND BlogPostId = @blogPostId
GO

CREATE PROCEDURE RemoveHashTagFromBlogPost @hashTagId INT, @blogPostId INT
AS
DELETE FROM BlogPostsHashTags
WHERE @hashtagId = HashTagId AND @blogPostId = BlogPostId
GO


--approval sprocs
CREATE PROCEDURE ApproveBlog @blogPostId INT
AS
UPDATE BlogPosts
SET Approved = 1
WHERE BlogPostId = @blogPostId
GO

CREATE PROCEDURE ApproveHashTag @hashTagId INT, @blogPostId INT
AS
UPDATE BlogPostsHashTags
SET  Approved = 1
WHERE BlogPostId = @blogPostId AND HashTagId = @hashTagId
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

CREATE PROCEDURE UpdateBlogPost @blogPostId INT, @blogPost TEXT
AS
UPDATE BlogPosts
SET Blog = @blogPost
WHERE BlogPostId = @blogPostId
GO

CREATE PROCEDURE UpdateCatagory @catagoryId INT, @catagoryName NVARCHAR(50)
AS
UPDATE Catagories
SET CatagoryName = @catagoryName
WHERE CatagoryId = @catagoryId
GO

CREATE PROCEDURE UpdateBlogger @bloggerId INT, @bloggerName NVARCHAR(100)
AS
UPDATE Bloggers
SET BloggerName = @bloggerName
WHERE BloggerId = @bloggerId
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
INSERT INTO BlogPostsCatagories (CatagoryId, BlogPostId)
VALUES ((SELECT CatagoryId FROM Catagories WHERE CatagoryId = @catagoryId), (SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = @blogPostId))
GO

CREATE PROCEDURE AddBloggerToBlogPost @bloggerId INT, @blogPostId INT
AS
INSERT INTO BlogPostsBloggers (BlogPostId, BloggerId)
VALUES ((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = @blogPostId), (SELECT BloggerId FROM Bloggers WHERE BloggerId = @bloggerId))
GO