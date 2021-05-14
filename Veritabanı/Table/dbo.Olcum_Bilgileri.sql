CREATE TABLE [dbo].[Olcum_Bilgileri] (
    [tc_no]     VARCHAR (50) NOT NULL,
    [kg]        REAL         NOT NULL,
    [boy]       INT          NOT NULL,
    [yag_orani] REAL         NOT NULL,
    [yas]       INT          NOT NULL,
    [vki]       REAL         NOT NULL,
    CONSTRAINT [PK_Olcum_Bilgileri] PRIMARY KEY CLUSTERED ([tc_no] ASC),
    FOREIGN KEY ([tc_no]) REFERENCES [dbo].[Abone] ([tc_no])
);

