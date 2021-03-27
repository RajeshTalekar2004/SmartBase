CREATE TABLE [dbo].[TransactionMaster] (
    [TrxId]      NCHAR (1)     NOT NULL,
    [DrCr]       NCHAR (1)     NOT NULL,
    [TrxDetail]  NVARCHAR (50) NULL,
    [AccountId1] NCHAR (16)    NULL,
    [AccountId2] NCHAR (16)    NULL,
    [AccountId3] NCHAR (16)    NULL,
    CONSTRAINT [PK_TransactionMaster] PRIMARY KEY CLUSTERED ([TrxId] ASC)
);

