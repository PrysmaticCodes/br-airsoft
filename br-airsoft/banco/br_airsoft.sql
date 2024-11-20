CREATE DATABASE br_airsoft;
USE br_airsoft;


-- Criando a tabela de Administradores
CREATE TABLE administrador (
    id_admin INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(50) UNIQUE NOT NULL,
    nome VARCHAR(50) NOT NULL,
    senha VARCHAR(50) NOT NULL );
		
        -- Adicionando um administrador no banco 
        INSERT INTO administrador ( email, nome, senha)
		VALUES ('admin@gmail.com', 'admin', 'admin123');


-- Criando a tabela de Clientes
CREATE TABLE cliente (
	id_cliente int auto_increment primary key not null, 
    cpf VARCHAR(11) not null,
    nome_cliente VARCHAR(50) NOT NULL,
    telefone VARCHAR(11), 
    email VARCHAR(50), 
    senha VARCHAR (50)
);

	-- Adicionando um cliente ao banco 
	INSERT INTO cliente ( cpf, nome_cliente, telefone, email, senha)
	VALUES ('45698723423', 'Fernando Oliveira', '11988888888', 'fernando@gmail.com', 'fernando123');

-- Criando a tabela de Endereço do cliente 
CREATE TABLE endereco(
	cep  VARCHAR(9) primary key, 
    estado VARCHAR(2),
    bairro VARCHAR(50),
    rua VARCHAR(90),
     id_cliente INT not null, -- ligando o cliente ao endereço 
    FOREIGN KEY (id_cliente) REFERENCES cliente (id_cliente)
    );
    
    -- Adicionando o endereço do cliente 
		INSERT INTO endereco ( cep, estado, bairro, rua, id_cliente)
		VALUES ('05333-070', 'SP',  'Jaguaré', 'tucano 350', 1);
        
        -- Criando a tabela de produtos 

CREATE TABLE Produto (
    id_prod INT AUTO_INCREMENT PRIMARY KEY,
    nome_prod VARCHAR(70) NOT NULL,
    desc_prod TEXT,
    valor_prod DECIMAL(10,2) NOT NULL,
    quant_produto int
);		
		-- Adicionando um produto ao banco 
		INSERT INTO produto (nome_prod, desc_prod, valor_prod, quant_produto)
		VALUES (
			'Pistola de Pressão Babayaga', 
			'Possui o sistema Blowback (GBB) que simula o recuo da arma de fogo, porém com menor potência, ou seja, cada disparo terá alto realismo, aumentando de forma absurda a semelhança com armas de fogo.', 
			1600.00, 
			20
			  );

 -- Criando a tabela item que ligará o  produto ao carrinho 
CREATE TABLE item (
    id_prod INT,
    quant_produto INT NOT NULL,
    id_cliente INT,
    FOREIGN KEY (id_prod) REFERENCES Produto(id_prod),
    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
);

 -- Adicionando o produto de id 1 ao cliente de id 1 com 2 vezes o mesmo item 
		INSERT INTO item (id_cliente, id_prod, quant_produto)
		VALUES (1, 1, 2);



-- criando a procedure com informações do cliente 

DELIMITER $$

CREATE PROCEDURE info_cliente (IN cliente_id INT)
BEGIN
    SELECT 
        c.nome_cliente AS Nome,
        c.cpf AS CPF,
        c.telefone AS Telefone,
        e.cep AS CEP,
        e.estado AS Estado,
        e.bairro AS Bairro,
        e.rua AS Rua
    FROM 
        cliente c
    LEFT JOIN 
        endereco e ON c.id_cliente = e.id_cliente
    WHERE 
        c.id_cliente = cliente_id;
END$$

DELIMITER ;

-- chamado as informações do cliente 

CALL info_cliente(1); -- Substitua "1" pelo ID do cliente desejado.

-- criando a procedure com informações do produto 

DELIMITER $$
CREATE PROCEDURE info_produto ()
BEGIN
    SELECT 
        id_prod AS ID,
        nome_prod AS Nome,
        valor_prod AS Preco,
        quant_produto AS Quantidade
    FROM 
        produto;
END$$

DELIMITER ;

CALL info_produto();


DELIMITER $$

-- criando procedure das informações de compra do cliente 

CREATE PROCEDURE carrinho(IN cliente_id INT)
BEGIN
    SELECT 
        c.id_cliente AS ID_Cliente,
        c.nome_cliente AS Nome,
        c.telefone AS Telefone,
        e.cep AS CEP,
        e.estado AS Estado,
        e.bairro AS Bairro,
        e.rua AS Rua,
        i.quant_produto AS Quantidade_Produto,  -- Quantidade do produto comprado
        p.nome_prod AS Nome_Produto,  -- Nome do produto
        (i.quant_produto * p.valor_prod) AS Total_Item,  -- Total do produto comprado
        COALESCE(SUM(i.quant_produto), 0) AS Total_Itens_Comprados,  -- Total de itens comprados
        COALESCE(SUM(i.quant_produto * p.valor_prod), 0) AS Valor_Total_Compra  -- Valor total gasto
    FROM 
        cliente c
    LEFT JOIN 
        endereco e ON c.id_cliente = e.id_cliente
    LEFT JOIN 
        item i ON c.id_cliente = i.id_cliente  -- Tabela item para verificar as compras
    LEFT JOIN 
        produto p ON i.id_prod = p.id_prod  -- Tabela produto para pegar os preços e nomes
    WHERE 
        c.id_cliente = cliente_id
    GROUP BY 
        c.id_cliente, c.nome_cliente, c.telefone, e.cep, e.estado, e.bairro, e.rua, i.id_prod, p.nome_prod, i.quant_produto;
END$$

DELIMITER ;

 CALL carrinho(1); -- Substitua "1" pelo ID de um cliente válido.
