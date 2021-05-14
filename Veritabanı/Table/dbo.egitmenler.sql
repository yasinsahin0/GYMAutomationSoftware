CREATE TABLE [dbo].[egitmenler] (
    [egt_tc_no] VARCHAR (50) NOT NULL,
    [ad]        NCHAR (15)   NOT NULL,
    [soyad]     NCHAR (15)   NOT NULL,
    [uzmanlik]  NCHAR (10)   NOT NULL,
    CONSTRAINT [PK_egitmenler] PRIMARY KEY CLUSTERED ([egt_tc_no] ASC)
);

