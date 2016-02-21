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
		@ClienteId = ClienteId
	From
		Venda
	Where 
		VendaId = @VendaId

	Select
		@ItemNumero = Count(*),
		@Total1 = Sum(Preco)
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
