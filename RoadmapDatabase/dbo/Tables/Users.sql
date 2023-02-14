CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Bio] NVARCHAR(1000) NULL,
    [Password] NVARCHAR(50) NULL, 
    [PasswordHash] VARBINARY(MAX) NULL, 
    [PasswordSalt] VARBINARY(MAX) NULL, 
    [CreatedDate] DATETIME2 NULL
)
