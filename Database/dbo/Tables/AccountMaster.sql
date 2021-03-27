CREATE TABLE [dbo].[AccountMaster] (
    [CompCode]  NCHAR (2)  NOT NULL,
    [AccYear]   NCHAR (4)  NOT NULL,
    [AccountId] NCHAR (16) NOT NULL,
    [Opening]   MONEY      NULL,
    [CurDr]     MONEY      NULL,
    [CurCr]     MONEY      NULL,
    [Closing]   MONEY      NULL,
    CONSTRAINT [PK_AccountMaster] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [AccountId] ASC)
);

