-- 4.1 Trigger para atualizar o estoque
Create Trigger trgAtualizarEstoque
On VendaProduto
For Insert
As
Declare
	@QteProdutoEstoque int,
	@QteProdutoPedido int,
	@ProdutoId int
Begin
	Select
		@ProdutoId = ProdutoId,
		@QteProdutoPedido = Quantidade
	From
		inserted

	Select
		@QteProdutoEstoque = Estoque
	From
		Produto
	Where
		ProdutoId = @ProdutoId

	if (@QteProdutoEstoque is null or @QteProdutoPedido > @QteProdutoEstoque)
		Rollback Transaction
		RAISERROR('Não tem produto suficiente no estoque!',16,1)
	else
		declare @NovaQuantidade int = @QteProdutoEstoque - @QteProdutoPedido
		exec spAtualizarEstoque @ProdutoId, @NovaQuantidade
End