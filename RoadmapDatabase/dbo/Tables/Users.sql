CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Bio] NVARCHAR(1000) NULL,
    [Password] NVARCHAR(50) NULL,
    [ConfirmationCode] UNIQUEIDENTIFIER NULL, 
    [ConfirmationCodeExpirationDate] datetime2(7) NULL, 
    [RestorationCode] UNIQUEIDENTIFIER NULL,
    [RestorationCodeExpirationDate] datetime2(7) NULL,
    [PasswordHash] VARBINARY(MAX) NULL, 
    [PasswordSalt] VARBINARY(MAX) NULL, 
    [IsAdmin] BIT NULL DEFAULT 0,
    [IsConfirmed] BIT NULL DEFAULT 0,
    [IsBanned] BIT NULL DEFAULT 0,
    [CreatedDate] DATETIME2 NULL, 
    [UpdatedDate] DATETIME2 NULL,

)
