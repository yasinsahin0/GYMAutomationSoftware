CREATE PROCEDURE [dbo].[AboneFilter]
	@cinsiyet varchar(50) ,
	@sure int
AS
begin
	select * from abone where cinsiyet = @cinsiyet and abone_suresi = @sure;
end