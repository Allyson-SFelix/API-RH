CREATE TABLE funcionarios(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(255) NOT NULL,
	dataEntrada DATE NOT NULL,
	cpf VARCHAR(11) UNIQUE NOT NULL,
	id_setor INTEGER NOT NULL,
	FOREIGN KEY (id_setor) REFERENCES setores(id),
	salario FLOAT NOT NULL,
	data_nascimento DATE,
	status enum_status NOT NULL
);

CREATE TABLE setores(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(255) UNIQUE NOT NULL,
	qtd_funcionarios INTEGER,
	localizacao VARCHAR(255),
	status enum_status NOT NULL
);

CREATE TABLE users(
	id SERIAL PRIMARY KEY,
	username VARCHAR(255) UNIQUE NOT NULL,
	senha_hash TEXT NOT NULL,
	status enum_status NOT NULL,
	roles enum_roles NOT NULL
);

CREATE TABLE movimentacao_users(
	id SERIAL PRIMARY KEY,
	data_modificacao DATE NOT NULL,
	dados_anterior TEXT NOT NULL, /*Provavel que passe json da linha mexida*/
	atos_efetuados TEXT NOT NULL,
	tabela_afetada VARCHAR(255) NOT NULL,
	id_users INTEGER NOT NULL,
	FOREIGN KEY (id_users) REFERENCES users(id)
);

