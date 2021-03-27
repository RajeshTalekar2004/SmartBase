﻿CREATE TABLE [dbo].[VoucherMaster] (
    [CompCode]   NCHAR (2)     NOT NULL,
    [AccYear]    NCHAR (4)     NOT NULL,
    [VouNo]      NCHAR (10)    NOT NULL,
    [VouDate]    DATE          NOT NULL,
    [TrxType]    NCHAR (1)     NOT NULL,
    [BilChq]     NVARCHAR (16) NULL,
    [BillId]     NCHAR (16)    NULL,
    [AccountId]  NCHAR (16)    NOT NULL,
    [DrCr]       NCHAR (1)     NOT NULL,
    [VouDetail]  NVARCHAR (50) NULL,
    [NetAmount]  MONEY         NULL,
    [SgstId]     INT           NULL,
    [SgstAmount] MONEY         NULL,
    [CgstId]     INT           NULL,
    [CgstAmount] MONEY         NULL,
    CONSTRAINT [PK_VoucherMaster] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC, [VouNo] ASC),
    CONSTRAINT [FK_VoucherMaster_AccountMaster] FOREIGN KEY ([CompCode], [AccYear], [AccountId]) REFERENCES [dbo].[AccountMaster] ([CompCode], [AccYear], [AccountId]),
    CONSTRAINT [FK_VoucherMaster_BillMaster] FOREIGN KEY ([CompCode], [AccYear], [BillId]) REFERENCES [dbo].[BillMaster] ([CompCode], [AccYear], [BillId]),
    CONSTRAINT [FK_VoucherMaster_CgstMaster] FOREIGN KEY ([CgstId]) REFERENCES [dbo].[CgstMaster] ([CgstId]),
    CONSTRAINT [FK_VoucherMaster_CompInfo] FOREIGN KEY ([CompCode], [AccYear]) REFERENCES [dbo].[CompInfo] ([CompCode], [AccYear]),
    CONSTRAINT [FK_VoucherMaster_SgstMaster] FOREIGN KEY ([SgstId]) REFERENCES [dbo].[SgstMaster] ([SgstId]),
    CONSTRAINT [FK_VoucherMaster_TypeMaster] FOREIGN KEY ([CompCode], [AccYear], [TrxType]) REFERENCES [dbo].[TypeMaster] ([CompCode], [AccYear], [TrxCd])
);
