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
    quant_produto int,
	caminho_imagem VARCHAR(150)
);		


		-- Adicionando um produto ao banco 
		INSERT INTO produto (nome_prod, desc_prod, valor_prod, quant_produto, caminho_imagem)
		VALUES (
			'Pistola teste de imagem' , 
			'Possui o sistema Blowback (GBB) que simula o recuo da arma de fogo, porém com menor potência, ou seja, cada disparo terá alto realismo, aumentando de forma absurda a semelhança com armas de fogo.', 
			1800.00, 
			1, 
            'E:\TCC\tcc\br-airsoft\br-airsoft\imagens.top-gun.png'
			  );
              
          

 -- Criando a tabela item que ligará o  produto ao carrinho 
CREATE TABLE compra (
	cod_compra int Primary Key auto_increment,
    data_compra  datetime , 
    id_cliente INT,
    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
   );
   
    -- Adicionando o produto de Ao carrinho  
		INSERT INTO compra (cod_compra,  data_compra, id_cliente)
		VALUES (1, now(), 1);

	
    CREATE TABLE itens_compra (
    nf int primary key auto_increment, 
    quant_produto int not null,
	cod_compra int, 
    id_cliente int, 
    id_prod int, 
    FOREIGN KEY (cod_compra) REFERENCES compra(cod_compra), 
	FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente), 
	FOREIGN KEY (id_prod) REFERENCES produto(id_prod)
    );
    
	-- Adicionando itens ao carrinho do cliente 
    INSERT INTO itens_compra (quant_produto,  cod_compra, id_cliente, id_prod)
		VALUES (2, 1, 1,1 );
 


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

CREATE PROCEDURE info_produto()
BEGIN
    SELECT 
        id_prod AS ID,
        nome_prod AS Nome,
        desc_prod AS Descricao,
        valor_prod AS Preco,
        quant_produto AS Quantidade,
        caminho_imagem AS Caminho_Imagem -- Inclui o caminho da imagem do produto
    FROM 
        produto;
END$$

DELIMITER ;


CALL info_produto();



DELIMITER $$

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
        co.data_compra AS Data_Compra, -- Data e horário da compra
        i.quant_produto AS Quantidade_Produto,  -- Quantidade do produto comprado
        p.nome_prod AS Nome_Produto,  -- Nome do produto
        p.valor_prod AS Preco_Unitario, -- Preço unitário do produto
        (i.quant_produto * p.valor_prod) AS Total_Item,  -- Total do produto comprado
        p.caminho_imagem AS Caminho_Imagem -- Caminho da imagem do produto
    FROM 
        cliente c
    LEFT JOIN 
        endereco e ON c.id_cliente = e.id_cliente
    LEFT JOIN 
        compra co ON c.id_cliente = co.id_cliente -- Tabela compra
    LEFT JOIN 
        itens_compra i ON co.cod_compra = i.cod_compra -- Tabela itens_compra
    LEFT JOIN 
        produto p ON i.id_prod = p.id_prod -- Tabela produto
    WHERE 
        c.id_cliente = cliente_id
    GROUP BY 
        c.id_cliente, c.nome_cliente, c.telefone, e.cep, e.estado, e.bairro, e.rua, 
        co.data_compra, i.quant_produto, p.nome_prod, p.valor_prod, p.caminho_imagem;
END$$

DELIMITER ;



 CALL carrinho(1); -- Substitua "1" pelo ID de um cliente válido.