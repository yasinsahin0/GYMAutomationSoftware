CREATE TABLE [dbo].[Abone] (
    [tc_no]        VARCHAR (50) NOT NULL,
    [ad]           VARCHAR (50) NOT NULL,
    [soyad]        VARCHAR (50) NOT NULL,
    [tel_no]       VARCHAR (50) NOT NULL,
    [cinsiyet]     VARCHAR (50) NOT NULL,
    [abone_suresi] INT          NOT NULL,
    [ucret]        INT          NOT NULL,
    [egitmen_tc]   VARCHAR (50) NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([tc_no] ASC)
);

