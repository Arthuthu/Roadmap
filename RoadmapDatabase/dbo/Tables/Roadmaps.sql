CREATE TABLE [dbo].[Roadmaps]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(200) NULL,
    [Category] NVARCHAR(50) NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [IsApproved] INT NULL, 
    [IsHidden] INT NULL, 
    [CreatedDate] DATETIME2 NULL,
    
    [UpdatedDate] DATETIME2 NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE

)