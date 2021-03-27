CREATE TABLE [dbo].[BillMaster] (
    [CompCode]  NCHAR (2)  NOT NULL,
    [AccYear]   NCHAR (4)  NOT NULL,
    [BillId]    NCHAR (16) NOT NULL,
    [AccountId] NCHAR (16) NOT NULL,
    [BillDate]  DATE       NOT NULL,
    [Amount]    MONEY      NOT NULL,
    [Adjusted]  MONEY      NULL,
    [Balance]   MONEY      NULL,
    CONSTRAINT [PK_BillMaster_1] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [BillId] ASC)
);

