CREATE TABLE funcionarios(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(255),
	dataEntrada DATE,
	cpf VARCHAR(11) UNIQUE,
	id_setor INTEGER,
	FOREIGN KEY (id_setor) REFERENCES setores(id),
	salario FLOAT,
	data_nascimento DATE,
	status enum_status
);

CREATE TABLE setores(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(255) UNIQUE,
	qtd_funcionarios INTEGER,
	localizacao VARCHAR(255),
	status enum_status
);

CREATE TABLE users(
	id SERIAL PRIMARY KEY,
	username VARCHAR(255) UNIQUE,
	senha_hash TEXT,
	status enum_status,
	roles enum_roles
);

CREATE TABLE movimentacao_users(
	id SERIAL PRIMARY KEY,
	data_modificacao DATE,
	dados_anterior TEXT, /*Provavel que passe json da linha mexida*/
	atos_efetuados TEXT,
	tabela_afetada VARCHAR(255),
	id_users INTEGER,
	FOREIGN KEY (id_users) REFERENCES users(id)
);

