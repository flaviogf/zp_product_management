CREATE TABLE [dbo].[Files] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(250),
    [Path] NVARCHAR(250),
    [Extension] NVARCHAR(250),
    PRIMARY KEY (Id)
)