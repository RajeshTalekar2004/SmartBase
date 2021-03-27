CREATE TABLE [dbo].[BillDetail] (
    [CompCode] NCHAR (2)  NOT NULL,
    [AccYear]  NCHAR (4)  NOT NULL,
    [BillId]   NCHAR (16) NOT NULL,
    [ItemSr]   INT        NOT NULL,
    [VouNo]    NCHAR (10) NOT NULL,
    [Amount]   MONEY      NOT NULL,
    CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [BillId] ASC, [ItemSr] ASC),
    CONSTRAINT [FK_BillDetail_BillMaster] FOREIGN KEY ([CompCode], [AccYear], [BillId]) REFERENCES [dbo].[BillMaster] ([CompCode], [AccYear], [BillId])
);

