-- 7 View para Listar os Cientes VIP's
Create View vwClientesVip
as
Select
	*
From
	Cliente
Where
	Vip = 1
  