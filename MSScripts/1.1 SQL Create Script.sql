USE master
go
IF EXISTS(select * from sys.databases where name='POS_Lista11')
	BEGIN
	DROP DATABASE POS_Lista11
	END
go
Create Database POS_Lista11
go
use POS_Lista11
go
Create table Fabricante(
FabricanteId int identity,
Descricao varchar(100) not null,
primary key(FabricanteId))
go
Create table Produto(
ProdutoId int identity,
Descricao varchar(100) not null,
FabricanteId int not null,
QtdEstoque int not null,
Preco decimal(18,2) not null,
primary key (ProdutoId),
foreign key (FabricanteId) references Fabricante(FabricanteId))
go
Create Table Cliente(
ClienteId int identity,
Nome varchar(100) not null,
Vip bit,
primary key (ClienteId))
go
Create Table Venda(
VendaId int identity,
ClienteId int not null,
Data datetime default getdate(),
Total1 decimal(18,2),
Total2 decimal(18,2),
Desconto decimal(18,2),
primary key(VendaId),
foreign key (ClienteId) references Cliente(ClienteId))
go
Create Table VendaProduto(
VendaId int,
ItemNumero int,
ProdutoId int,
Quantidade int not null,
Preco decimal(18,2) not null,
primary key(VendaId,ItemNumero),
foreign key (VendaId) references Venda(VendaId),
foreign key (ProdutoId) references Produto(ProdutoId))