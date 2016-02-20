USE master
go
IF EXISTS(select * from sys.databases where name='POS_Lista11')
Use POS_Lista11
go
-- Insert da tabela Cliente
Insert into Cliente(Nome,Vip)
Values
	('Francisco Bento',1),
	('Felipe Mateus',0),
	('Gabriel Bessa',1),
	('Beatriz Silva',0),
	('José Afrânio',0)
	
-- Insert da tabela Fabricante
Insert into Fabricante(Descricao)
Values
	('Tramontina'),
	('DELL'),
	('Microsoft'),
	('Google'),
	('Apple')

-- Insert da tabela Produto
Insert into Produto(Descricao,FabricanteId,Preco,QtdEstoque)
Values
	('Faca super amolada',1,69.70,50),
	('Ultrabook i7 32GB RAM',2,3499.98,30),
	('Pacote Windows 10',3,799,100),
	('Android Nexus S7',4,1299.36,65),
	('iPhone 6S',5,2359,20)
	
	