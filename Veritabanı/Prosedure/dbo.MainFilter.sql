CREATE PROCEDURE [dbo].[MainFilter]
	@cinsiyet varchar(50),
	@sure int,
	@saglik varchar(50)
	
AS
begin

select * from Abone where tc_no in(
	SELECT tc_no from Saglik_Bilgileri 
	where saglik_durumu=@saglik and Saglik_Bilgileri.tc_no in(
			select tc_no from Abone where cinsiyet=@cinsiyet and abone_suresi=@sure));
end