CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [PasswordHash] VARBINARY(MAX) NULL, 
    [PasswordSalt] VARBINARY(MAX) NULL 
)
