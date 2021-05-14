CREATE PROCEDURE [dbo].[AboneEgitmenGetir]
	

AS
begin
	declare @param1 varchar(50) 
set @param1 = (SELECT soyad from egitmenler)

RETURN @param1
end