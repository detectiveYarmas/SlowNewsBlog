USE master
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name='SlowNewsBlog')
DROP DATABASE SlowNewsBlog
GO

CREATE DATABASE SlowNewsBlog
GO

USE SlowNewsBlog
GO
