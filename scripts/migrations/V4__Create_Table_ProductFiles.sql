CREATE TABLE [dbo].[ProductFiles] (
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [FileId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (ProductId, FileId),
    FOREIGN KEY (ProductId) REFERENCES [dbo].[Products] (Id),
    FOREIGN KEY (FileId) REFERENCES [dbo].[Files] (Id)
)