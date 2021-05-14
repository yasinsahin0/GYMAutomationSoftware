CREATE PROCEDURE [dbo].[MaliyetFilter]
	@date varchar(50),
	@person varchar(50),
	@giderilk int,
	@giderson int
AS
begin
	SELECT * from Giderler where tarih=@date and islem_yapan =@person and gider>@giderilk and gider < @giderson
end