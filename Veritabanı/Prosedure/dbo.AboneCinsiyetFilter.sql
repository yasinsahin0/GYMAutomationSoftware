CREATE PROCEDURE [dbo].[AboneCinsiyetFilter] (@cinsiyet  varchar(50))
as
begin
	SELECT * from Abone where cinsiyet =@cinsiyet;
end