-- 5.1 Store Procedure para listagem de Vendas por Período
Create Procedure spVendasPorPeriodo
	@DataInicio datetime,
	@DataFim datetime
As
Begin
	Select
		*
	From 
		Venda
	Where 
		Data Between @DataInicio and @DataFim
End

	