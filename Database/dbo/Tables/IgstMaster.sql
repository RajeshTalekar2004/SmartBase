CREATE TABLE [dbo].[IgstMaster]
(
	[IgstId] INT NOT NULL , 
    [IgstDetail] NVARCHAR(50) NOT NULL, 
    [IgstRate] MONEY NOT NULL,
	CONSTRAINT [PK_IgstMaster] PRIMARY KEY CLUSTERED ([IgstId] ASC)
)
