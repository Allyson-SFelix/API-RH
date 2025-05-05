# Scripts SQL

Antes de rodar a API, execute os arquivos SQL na pasta `/sql` na ordem:

1. `001_create_DB.sql` � cria o schema do banco (se necess�rio)
2. `002_create_enums.sql` � cria os enums status e roles
3. `003_create_tables.sql` � cria as tabelas funcionarios,setores,users e logs que s�o utilizadas pela API
4. `004_create_functions.sql` � cria as fun��es relacionadas com a l�gica de incremento e decremento da quantidade de funcionarios de cada setor 
5. `005_create_tables.sql` � cria as triggers para possibilitar a execu��o das fun��es criadas quando houver determinadas a��es
> Banco recomendado: PostgreSQL