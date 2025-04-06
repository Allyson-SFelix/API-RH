# Scripts SQL

Antes de rodar a API, execute os arquivos SQL na pasta `/sql` na ordem:

1. `001_create_DB.sql` – cria o schema do banco (se necessário)
2. `002_create_enums.sql` – cria os enums status e roles
3. `003_create_tables.sql` – cria as tabelas funcionarios,setores,users e logs que são utilizadas pela API
> Banco recomendado: PostgreSQL