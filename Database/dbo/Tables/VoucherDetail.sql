CREATE TABLE [dbo].[VoucherDetail] (
    [CompCode]  NCHAR (2)     NOT NULL,
    [AccYear]   NCHAR (4)     NOT NULL,
    [VouNo]     NCHAR (10)    NOT NULL,
    [ItemSr]    INT           NOT NULL,
    [AccountId] NCHAR (16)    NOT NULL,
    [DrCr]      NCHAR (1)     NOT NULL,
    [Amount]    MONEY         NOT NULL,
    [VouDetail] NVARCHAR (50) NULL,
    CONSTRAINT [PK_VoucherDetail] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [VouNo] ASC, [ItemSr] ASC),
    CONSTRAINT [FK_VoucherDetail_AccountMaster] FOREIGN KEY ([CompCode], [AccYear], [AccountId]) REFERENCES [dbo].[AccountMaster] ([CompCode], [AccYear], [AccountId]),
    CONSTRAINT [FK_VoucherDetail_VoucherMaster] FOREIGN KEY ([CompCode], [AccYear], [VouNo]) REFERENCES [dbo].[VoucherMaster] ([CompCode], [AccYear], [VouNo])
);

