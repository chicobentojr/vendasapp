-- Operações para o Banco

-- 2.1 Store Procedure para cadastrar uma Venda
Create Procedure spVendaCadastro
	@ClienteId Int,
	@Data Datetime,
	@VendaId int OUTPUT
As
Declare 
		@Total2 Decimal(18,2),
		@Desconto Decimal(18,2),
		@Vip Bit,
		@QteVendas Int
Begin
	Select
		@Vip = Vip 
	From 
		Cliente 
	Where 
		ClienteId = @ClienteId
	If(@Vip = 1)
		Set @Desconto = 15
	Else
		Begin
		Select 
			@QteVendas = COUNT(*) 
		From 
			Venda 
		Where 
			ClienteId = @ClienteId 
		And DateAdd(Month,-6,getdate()) < Data
		
		If(@QteVendas >= 10)
			Set @Desconto = 10
		Else
			Set @Desconto = @QteVendas
		End

	Insert Into Venda(ClienteId,Data)
	Values(@ClienteId,@Data)

	set @VendaId = SCOPE_IDENTITY()
End

Go
-- 2.2 Store Procedure para cadastrar um Produto da Venda
Create Procedure spVendaProdutoCadastro
	@VendaId int,
	@ProdutoId int,
	@Quantidade int,
	@ItemNumero int OUTPUT
As
Declare 
		@ClienteId int,
		@Total1 Decimal(18,2),
		@Total2 Decimal(18,2),
		@Desconto Decimal(18,2),
		@Vip Bit,
		@QteVendas Int,
		@PrecoProduto decimal(18,2),
		@PrecoTotal decimal(18,2)
Begin
	Select
		@ClienteId = ClienteId,
		@Total1 = Total1
	From
		Venda
	Where 
		VendaId = @VendaId

	Select
		@ItemNumero = Count(*)
	From
		VendaProduto
	Where 
		VendaId = @VendaId
		
	if(@ItemNumero > 0)
		set @ItemNumero = @ItemNumero + 1
	else
		set @ItemNumero = 1

	Select
		@PrecoProduto = Preco
	From
		Produto
	Where
		ProdutoId = @ProdutoId

	set @PrecoTotal = @PrecoProduto * @Quantidade

	Insert into VendaProduto(VendaId,ItemNumero,ProdutoId,Quantidade,Preco)
	Values(@VendaId,@ItemNumero,@ProdutoId,@Quantidade,@PrecoTotal)

	if(@Total1 is null)
		set @Total1 = 0
	set @Total1 = @Total1 + @PrecoTotal

	Select
		@Vip = Vip 
	From 
		Cliente 
	Where 
		ClienteId = @ClienteId

	If(@Vip = 1)
		Set @Desconto = 15
	Else
		Begin
		Select 
			@QteVendas = COUNT(*) 
		From 
			Venda 
		Where 
			ClienteId = @ClienteId 
		And DateAdd(Month,-6,getdate()) < Data
		
		If(@QteVendas >= 10)
			Set @Desconto = 10
		Else
			Set @Desconto = @QteVendas
		End
			
	Set @Total2 = @Total1 - (@Desconto * @Total1 / 100)			
			
	Update Venda 
	set 
		Total1 = @Total1,
		Total2 = @Total2,
		Desconto = @Desconto
	Where
		VendaId = @VendaId
End

Go
-- 3.1 Store Procedure para definir Cliente Vip
Create Procedure spClienteVip
	@ClienteId Int
As
Declare 
	@QteVendas int,
	@Vip Bit
Begin
	Select
		@Vip = Vip
	From 
		Cliente
	Where 
		ClienteId = @ClienteId

	if(@Vip = 1)
		Begin
			Select
				@QteVendas = COUNT(*)
			From 
				Venda
			Where 
				ClienteId = @ClienteId
			and DateAdd(Day,-90,getdate()) < Data

			if (@QteVendas is null or @QteVendas = 0)
				Update Cliente
				set Vip = 0
				where ClienteId = @ClienteId
		End
	else
		Begin
			Select
				@QteVendas = COUNT(*)
			From 
				Venda
			Where 
				ClienteId = @ClienteId
			and DateAdd(Month,-12,getdate()) < Data

			if (@QteVendas >= 20)
				Update Cliente
				set Vip = 1
				where ClienteId = @ClienteId
		End
End
Go
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
Go
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
		@QteProdutoEstoque = QtdEstoque
	From
		Produto
	Where
		ProdutoId = @ProdutoId

	if (@QteProdutoEstoque is null or @QteProdutoPedido > @QteProdutoEstoque)
		Begin
		Rollback Transaction
		RAISERROR('Não tem produto suficiente no estoque!',16,1)
		End
	else
		Begin
		declare @NovaQuantidade int = @QteProdutoEstoque - @QteProdutoPedido
		exec spAtualizarEstoque @ProdutoId, @NovaQuantidade
		End
End
Go
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
Go
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

Go
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

	
Go
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

	
Go
-- 7 View para Listar os Cientes VIP's
Create View vwClientesVip
as
Select
	*
From
	Cliente
Where
	Vip = 1
  
Go