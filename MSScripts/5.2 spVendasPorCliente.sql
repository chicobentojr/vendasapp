-- 5.2 Store Procedure para listagem de Vendas por Cliente
Create Procedure spVendasPorCliente
	@ClienteId int
As
Begin
	Select
		*
	From 
		Venda
	Where 
		ClienteId = @ClienteId
End

	