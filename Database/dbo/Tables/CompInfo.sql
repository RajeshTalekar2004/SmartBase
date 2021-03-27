CREATE TABLE [dbo].[CompInfo] (
    [CompCode]    NCHAR (2)     NOT NULL,
    [AccYear]     NCHAR (4)     NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [YearBegin]   DATE          NOT NULL,
    [YearEnd]     DATE          NOT NULL,
    [TaxId]       NVARCHAR (50) NULL,
    [AutoVoucher] NCHAR (1)     NULL,
    [BillMatch]   NCHAR (1)     NULL,
    [Address]     NCHAR (50)    NULL,
    CONSTRAINT [PK_CompInfo] PRIMARY KEY CLUSTERED ([CompCode] ASC, [AccYear] ASC)
);

