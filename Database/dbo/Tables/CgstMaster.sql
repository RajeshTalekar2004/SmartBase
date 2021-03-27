CREATE TABLE [dbo].[CgstMaster] (
    [CgstId]     INT           NOT NULL,
    [CgstDetail] NVARCHAR (50) NOT NULL,
    [CgstRate]   MONEY         NOT NULL,
    CONSTRAINT [PK_CgstMaster] PRIMARY KEY CLUSTERED ([CgstId] ASC)
);

