CREATE TABLE [dbo].[Employees]
(
  [ID] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(32) NOT NULL, 
    [LastName] VARCHAR(32) NOT NULL, 
    [Email] VARCHAR(70) NOT NULL, 
    [Username] VARCHAR(20) NULL, 
    [Password] NCHAR(20) NULL
)