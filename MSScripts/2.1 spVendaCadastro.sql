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
