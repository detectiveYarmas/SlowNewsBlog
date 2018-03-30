USE SlowNewsBlog
GO

DROP TABLE IF EXISTS dbo.BlogPostsCatagories
DROP TABLE IF EXISTS dbo.BlogPostsBloggers
DROP TABLE IF EXISTS dbo.BlogPostsHashTags
DROP TABLE IF EXISTS dbo.Catagories
DROP TABLE IF EXISTS dbo.HashTags
DROP TABLE IF EXISTS dbo.BlogPosts
DROP TABLE IF EXISTS dbo.StaticPages
GO 

CREATE TABLE StaticPages
(
    StaticPageId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Body TEXT
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
	Id nvarchar(128) foreign key REFERENCES AspNetUsers(Id)
)
GO

CREATE TABLE HashTags
(
    HashTagId INT PRIMARY KEY IDENTITY(1,1),
    HashTagName NVARCHAR(50),
	Approved BIT DEFAULT 0
)
GO

CREATE TABLE Catagories
(
    CatagoryId INT PRIMARY KEY IDENTITY(1,1),
    CatagoryName NVARCHAR(50),
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



CREATE TABLE BlogPostsCatagories
(
    BlogPostId INT FOREIGN KEY REFERENCES BlogPosts(BlogPostId),
    CatagoryId INT FOREIGN KEY REFERENCES Catagories(CatagoryId),
    PRIMARY KEY (BlogPostId, CatagoryId)
)
GO