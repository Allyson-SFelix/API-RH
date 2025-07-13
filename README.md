# API RH — Gerenciamento com ASP.NET Core, JWT e Dapper/EF

API RESTful construída em C# com ASP.NET Core para gestão de usuários, setores e funcionários, utilizando arquitetura limpa e boas práticas de mercado.

---

##  Tecnologias Utilizadas

* **ASP.NET Core**
* **Entity Framework Core** (inserções)
* **Dapper** (seleções e updates performáticos)
* **PostgreSQL**
* **Autenticação JWT** aplicada em TODO o projeto
* **Criptografia de senha** (hash seguro)
* **MVC + DTOs**
* **Arquitetura em camadas**: Controller → Service → Repository
* **Queries externas organizadas em arquivos**

---

##  Endpoints Documentados

###  Funcionários

> Todas as rotas protegidas por JWT (token no header)

#### `POST /api/funcionarios/inserirFuncionario`

✅ Cadastra novo funcionário
⭕ Body: `FuncionarioRequest` (nome, cpf, dataEntrada, id\_setor, salario, dataNascimento)

#### `GET /api/funcionarios/listaFuncionarios`

✅ Lista todos os funcionários de um setor
⭕ Body: `{ "nome": "nomeDoSetor" }`

#### `GET /api/funcionarios/unitFuncionario`

✅ Retorna dados de um funcionário por CPF
⭕ Body: `{ "cpf": "12345678900" }`

#### `GET /api/funcionarios/idFuncionario`

✅ Retorna apenas o ID do funcionário com base no CPF
⭕ Body: `{ "cpf": "12345678900" }`

#### `PUT /api/funcionarios/atualizarFuncionario?id=1`

✅ Atualiza os dados de um funcionário
⭕ Body: `FuncionarioRequest` + id via query param

#### `DELETE /api/funcionarios/removerFuncionario`

✅ Remove (soft delete) um funcionário por CPF
⭕ Body: `{ "cpf": "12345678900" }`

---

###  Setores

> Todas as rotas protegidas por JWT

#### `POST /api/setores/salvarSetor`

✅ Cria um novo setor
⭕ Body: `SetoresRequest`

#### `GET /api/setores/listarSetores`

✅ Lista todos os setores cadastrados

#### `GET /api/setores/PegarSetor`

✅ Retorna os dados de um setor pelo nome
⭕ Body: `{ "nome": "Financeiro" }`

#### `PUT /api/setores/atualizarSetor?nome=Financeiro`

✅ Atualiza os dados de um setor pelo nome
⭕ Body: `SetoresRequest` + nome via query param

#### `DELETE /api/setores/removerSetor`

✅ Remove (soft delete) um setor pelo nome
⭕ Body: `{ "nome": "Financeiro" }`

---

###  Usuários

> Algumas rotas são públicas, outras requerem autenticação com roles

#### `POST /api/users/login`

✅ Autentica um usuário com username e senha
⭕ Body: `{ "username": "admin", "password": "admin" }`
🔓 Acesso: público

#### `POST /api/users/register`

✅ Registra novo usuário
⭕ Body: `UserRegisterRequest`
🔒 Acesso: apenas roles `admin`, `gerente`

---
