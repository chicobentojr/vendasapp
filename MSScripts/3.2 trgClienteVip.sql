-- 3.2 Trigger para definir Cliente Vip
Create Trigger trgClienteVip
On Venda
For Insert
As
Declare
	@ClienteId int
Begin
	Select
		@ClienteId = ClienteId
	From
		inserted

	exec spClienteVip @ClienteId
End
