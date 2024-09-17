CREATE DATABASE br_airsoft;
USE br_airsoft;


-- Criando a tabela de Administradores
CREATE TABLE administrador (
    id_admin INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(50) UNIQUE NOT NULL,
    nome VARCHAR(50) NOT NULL,
    senha VARCHAR(50) NOT NULL 
);

-- Criando a tabela de Clientes
CREATE TABLE cliente (
    cpf VARCHAR(11) PRIMARY KEY,
    nome_cliente VARCHAR(50) NOT NULL,
    telefone VARCHAR(11), 
    cep VARCHAR(9), 
    estado VARCHAR(2),
    bairro VARCHAR(50),
    rua VARCHAR(90),
    endereco VARCHAR(5),
    email VARCHAR(50), 
    senha VARCHAR (50)
);

-- Criando a tabela de Produtos
CREATE TABLE Produto (
    id_prod INT AUTO_INCREMENT PRIMARY KEY,
    nome_prod VARCHAR(70) NOT NULL,
    desc_prod TEXT,
    valor_prod DECIMAL(10,2) NOT NULL,
    cor_prod VARCHAR(20),
    id_admin INT,
    FOREIGN KEY (id_admin) REFERENCES administrador(id_admin)
);

-- Criando a tabela de Pedidos
CREATE TABLE Nota_fiscal (
    nota_fiscal INT AUTO_INCREMENT PRIMARY KEY,
    valor_total DECIMAL(10,2) NOT NULL,
    data_pedido DATE NOT NULL,
  forma_pagamento ENUM('pix', 'boleto', 'cartao_credito', 'cartao_debito'), 
    cpf VARCHAR(11),
    Status ENUM('pendente', 'em andamento', 'entregue', 'cancelado') DEFAULT 'pendente',
    id_admin INT,
    FOREIGN KEY (cpf) REFERENCES cliente(cpf),
    FOREIGN KEY (id_admin) REFERENCES administrador(id_admin)
);

-- Criando a tabela de Itens de Pedidos
CREATE TABLE item (
    id_item INT AUTO_INCREMENT PRIMARY KEY,
    nota_fiscal INT,
    id_prod INT,
    quantidade INT NOT NULL,
    FOREIGN KEY (nota_fiscal) REFERENCES nota_fiscal(nota_fiscal),
    FOREIGN KEY (id_prod) REFERENCES produto(id_prod)
);
CREATE TABLE carrinho (
    id_carrinho INT AUTO_INCREMENT PRIMARY KEY,
    cpf VARCHAR(11),
	id_prod INT,
    quantidade INT,
    criado_em TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
     FOREIGN KEY (cpf) REFERENCES cliente(cpf),
    FOREIGN KEY (id_prod) REFERENCES produto(id_prod)
);