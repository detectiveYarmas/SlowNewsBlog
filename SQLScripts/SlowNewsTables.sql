USE SlowNewsBlog
GO


DROP TABLE IF EXISTS dbo.BlogPostsBloggers
GO
DROP TABLE IF EXISTS dbo.BlogPostsHashTags
GO
DROP TABLE IF EXISTS dbo.BlogPosts
GO 
DROP TABLE IF EXISTS dbo.HashTags
GO
DROP TABLE IF EXISTS dbo.Catagories
GO
DROP TABLE IF EXISTS dbo.StaticPages
GO


CREATE TABLE StaticPages
(
    StaticPageId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Body TEXT
)
GO


CREATE TABLE Catagories
(
    CatagoryId INT PRIMARY KEY IDENTITY(1,1),
    CatagoryName NVARCHAR(50),
	Approved BIT DEFAULT 0
)
GO



CREATE TABLE BlogPosts
(
    BlogPostId INT PRIMARY KEY IDENTITY(1,1),
    Blog TEXT,
    Title NVARCHAR(100),
    Approved bit DEFAULT 0,
    PublishDate DATE,
    DateAdded DATE DEFAULT(GetDate()),
	CatagoryId int foreign key REFERENCES Catagories(CatagoryId),
	Id nvarchar(256) foreign key REFERENCES AspNetUsers(UserName),
	HeaderImage nvarchar(128)
)
GO

CREATE TABLE HashTags
(
    HashTagId INT PRIMARY KEY IDENTITY(1,1),
    HashTagName NVARCHAR(50),
	Approved BIT DEFAULT 0
)
GO

--bridge tables
CREATE TABLE BlogPostsHashTags
(
    HashTagId INT FOREIGN KEY REFERENCES HashTags(HashTagId),
    BlogPostId INT FOREIGN KEY REFERENCES BlogPosts(BlogPostId)
    PRIMARY KEY (HashTagId, BlogPostId),
    VoteCount INT DEFAULT 0,
    Approved bit DEFAULT 0
)
GO

