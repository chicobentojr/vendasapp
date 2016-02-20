-- 6 Store Procedure para listagem de Produtos mais vendidos por mês
Create Procedure spProdutosMaisVendidosPorMes
	@QuantidadeProdutos int
As
Begin
select top (@QuantidadeProdutos)
	P.ProdutoId,
	P.Descricao as 'Produto',
	F.Descricao as 'Fabricante',
	Sum(VP.Quantidade) as 'Vendidos'
from
	Produto P inner join VendaProduto VP
	on P.ProdutoId = VP.ProdutoId
	inner join Venda V
	on VP.VendaId = V.VendaId
	inner join Fabricante F
	on P.FabricanteId = F.FabricanteId
Where
    V.Data > DateAdd(month, DATEDIFF(month, 0, getdate()), 0)
Group by
	P.ProdutoId,
	P.Descricao,
	F.Descricao
Order by
	'Vendidos'
End

	