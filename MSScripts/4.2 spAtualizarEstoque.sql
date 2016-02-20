-- 4.2 Store Procedure para atualizar o estoque
Create Procedure spAtualizarEstoque
	@ProdutoId int,
	@Quantidade int
As
Update
	Produto
set
	QtdEstoque = @Quantidade
where
	ProdutoId = @ProdutoId