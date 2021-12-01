CREATE TABLE Animais(
	AnimalId int IDENTITY(1,1) NOT NULL,
	Descricao varchar(150) NOT NULL,
	Preco numeric(18, 2) NOT NULL,
 CONSTRAINT PK_Animais PRIMARY KEY CLUSTERED (AnimalId ASC))

CREATE TABLE Pecuaristas(
	PecuaristaId int IDENTITY(1,1) NOT NULL,
	Nome varchar(150) NOT NULL,
CONSTRAINT PK_Pecuaristas PRIMARY KEY CLUSTERED (PecuaristaId ASC))

CREATE TABLE PedidoItens(
	PedidoItemId int IDENTITY(1,1) NOT NULL,
	PedidoId int NOT NULL,
	AnimalId int NOT NULL,
	Quantidade int NOT NULL,
CONSTRAINT PK_PedidoItens PRIMARY KEY CLUSTERED (PedidoItemId ASC))

CREATE TABLE Pedidos(
	PedidoId int IDENTITY(1,1) NOT NULL,
	PecuaristaId int NOT NULL,
	DataEntrega datetime NOT NULL,
CONSTRAINT PK_Pedidos PRIMARY KEY CLUSTERED (PedidoId ASC))

INSERT Animais (Descricao, Preco) VALUES ('Bufalos', CAST(125.43 AS Numeric(18, 2)))
INSERT Animais (Descricao, Preco) VALUES ('Gado', CAST(657.59 AS Numeric(18, 2)))

INSERT Pecuaristas (Nome) VALUES ('Pecuarista A')
INSERT Pecuaristas (Nome) VALUES ('Pecuarista B')
INSERT Pecuaristas (Nome) VALUES ('Pecuarista C')
INSERT Pecuaristas (Nome) VALUES ('Pecuarista D')

INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (1, 1, 123)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (1, 2, 127)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (2, 2, 29)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (4, 1, 100)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (4, 2, 200)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (5, 1, 123)
INSERT PedidoItens (PedidoId, AnimalId, Quantidade) VALUES (5, 2, 357)

INSERT Pedidos (PecuaristaId, DataEntrega) VALUES (1, CAST('2020-10-10T00:00:00.000' AS DateTime))
INSERT Pedidos (PecuaristaId, DataEntrega) VALUES (2, CAST('2030-10-10T00:00:00.000' AS DateTime))
INSERT Pedidos (PecuaristaId, DataEntrega) VALUES (1, CAST('2030-10-10T03:00:00.000' AS DateTime))
INSERT Pedidos (PecuaristaId, DataEntrega) VALUES (2, CAST('2025-11-12T03:00:00.000' AS DateTime))

ALTER TABLE Pecuaristas  WITH CHECK ADD  CONSTRAINT FK_Pecuaristas_Pecuaristas FOREIGN KEY(PecuaristaId)
REFERENCES Pecuaristas (PecuaristaId)
ALTER TABLE Pecuaristas CHECK CONSTRAINT FK_Pecuaristas_Pecuaristas
ALTER TABLE PedidoItens  WITH CHECK ADD  CONSTRAINT FK_PedidoItens_Animais FOREIGN KEY(AnimalId)
REFERENCES Animais (AnimalId)
ALTER TABLE PedidoItens CHECK CONSTRAINT FK_PedidoItens_Animais
ALTER TABLE PedidoItens  WITH CHECK ADD  CONSTRAINT FK_PedidoItens_Pedidos FOREIGN KEY(PedidoId)
REFERENCES Pedidos (PedidoId)
ALTER TABLE PedidoItens CHECK CONSTRAINT FK_PedidoItens_Pedidos
ALTER TABLE Pedidos  WITH CHECK ADD  CONSTRAINT FK_Pedidos_Pecuaristas FOREIGN KEY(PecuaristaId)
REFERENCES Pecuaristas (PecuaristaId)
ALTER TABLE Pedidos CHECK CONSTRAINT FK_Pedidos_Pecuaristas