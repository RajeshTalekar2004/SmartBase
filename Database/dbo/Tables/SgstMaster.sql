CREATE TABLE [dbo].[SgstMaster] (
    [SgstId]     INT           NOT NULL,
    [SgstDetail] NVARCHAR (50) NOT NULL,
    [SgstRate]   MONEY         NOT NULL,
    CONSTRAINT [PK_SgstMaster] PRIMARY KEY CLUSTERED ([SgstId] ASC)
);

