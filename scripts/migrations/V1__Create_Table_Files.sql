CREATE TABLE [dbo].[Files]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Path] NVARCHAR(255) NOT NULL,
    [Ext] NVARCHAR(255) NOT NULL,
    PRIMARY KEY (Id)
);

GO