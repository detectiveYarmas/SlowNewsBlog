USE SlowNewsBlog
GO

CREATE LOGIN SuperBlogger WITH PASSWORD ='fart'
GO


CREATE USER blog_master FOR LOGIN SuperBlogger;
GO

ALTER ROLE db_owner ADD MEMBER blog_master
GO