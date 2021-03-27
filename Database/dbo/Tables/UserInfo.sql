CREATE TABLE [dbo].[UserInfo] (
    [CompCode]     NCHAR (2)     NOT NULL,
    [UserName]     NCHAR (25)    NOT NULL,
    [UserEmailId]  NVARCHAR (50) NULL,
    [UserPassword] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserInfo_1] PRIMARY KEY CLUSTERED ([UserName] ASC)
);

