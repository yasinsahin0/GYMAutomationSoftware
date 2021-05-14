CREATE TABLE [dbo].[Giderler] (
    [tarih]       DATETIME       NOT NULL,
    [fatura_no]   VARCHAR (50)   NOT NULL,
    [aciklama]    NVARCHAR (MAX) NULL,
    [islem_yapan] VARCHAR (50)   NOT NULL,
    [gider]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([fatura_no] ASC)
);

