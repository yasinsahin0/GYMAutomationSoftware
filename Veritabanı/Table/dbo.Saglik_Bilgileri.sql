CREATE TABLE [dbo].[Saglik_Bilgileri] (
    [hes_kodu]          NCHAR (20)   NOT NULL,
    [saglik_durumu]     NCHAR (10)   NOT NULL,
    [takviye_kullanimi] NCHAR (10)   NOT NULL,
    [tc_no]             VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([hes_kodu] ASC),
    FOREIGN KEY ([tc_no]) REFERENCES [dbo].[Abone] ([tc_no])
);

