CREATE TABLE [dbo].[Products] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(250) NOT NULL,
    [Description] NVARCHAR(500) NOT NULL,
    [Price] DECIMAL(18, 2) NOT NULL,
    [Quantity] INTEGER NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (CategoryId) REFERENCES [dbo].[Categories] (Id)
)