CREATE PROCEDURE [dbo].[MaliFaturaSorgulama]
	@fatura varchar(50)

AS
begin
SELECT * from Giderler where fatura_no=@fatura

end