CREATE TABLE [dbo].[TypeMaster] (
    [CompCode]  NCHAR (2)     NOT NULL,
    [AccYear]   NCHAR (4)     NOT NULL,
    [TrxCd]     NCHAR (1)     NOT NULL,
    [TrxDetail] NVARCHAR (50) NOT NULL,
    [Prefix]    NCHAR (4)     NOT NULL,
    [ItemSr]    INT           NULL,
    CONSTRAINT [PK_TypeMaster] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [TrxCd] ASC),
    CONSTRAINT [FK_TypeMaster_TransactionMaster] FOREIGN KEY ([TrxCd]) REFERENCES [dbo].[TransactionMaster] ([TrxId])
);

