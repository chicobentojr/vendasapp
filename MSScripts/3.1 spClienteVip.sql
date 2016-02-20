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

	