CREATE TABLE [dbo].[Ledger] (
    [CompCode]      NCHAR (2)  NOT NULL,
    [AccYear]       NCHAR (4)  NOT NULL,
    [VouNo]         NCHAR (10) NOT NULL,
    [VouDate]       DATE       NULL,
    [ItemSr]        INT        NOT NULL,
    [TrxType]       NCHAR (1)  NOT NULL,
    [BilChq]        NCHAR(16)  NULL,
    [AccountId]     NCHAR (16) NULL,
    [DrCr]          NCHAR (1)  NULL,
    [Amount]        MONEY      NULL,
    [CorrAccountId] NCHAR (16) NULL,
    [VouDetail]     NVARCHAR (50) NULL,
	CONSTRAINT [PK_Ledger] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [VouNo] ASC,[ItemSr] ASC),
    CONSTRAINT [FK_Ledger_VoucherMaster] FOREIGN KEY ([CompCode], [AccYear], [VouNo]) REFERENCES [dbo].[VoucherMaster] ([CompCode], [AccYear], [VouNo])
);

