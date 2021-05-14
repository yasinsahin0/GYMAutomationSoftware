CREATE TABLE [dbo].[Supplementler] (
    [tc_no]      VARCHAR (50) NOT NULL,
    [kreatin]    NCHAR (10)   NOT NULL,
    [protein]    NCHAR (10)   NOT NULL,
    [omega_3]    NCHAR (10)   NOT NULL,
    [bcaa]       NCHAR (10)   NOT NULL,
    [Lkarnitin]  NCHAR (10)   NOT NULL,
    [probiyotik] NCHAR (10)   NOT NULL,
    CONSTRAINT [PK_Supplementler] PRIMARY KEY CLUSTERED ([tc_no] ASC),
    FOREIGN KEY ([tc_no]) REFERENCES [dbo].[Abone] ([tc_no])
);

