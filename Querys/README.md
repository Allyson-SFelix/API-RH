# Scripts SQL

Antes de rodar a API, execute os arquivos SQL na pasta `/sql` na ordem:

1. `001_create_DB.sql` – cria o schema do banco (se necessário)
2. `002_create_enums.sql` – cria os enums status e roles
3. `003_create_tables.sql` – cria as tabelas funcionarios,setores,users e logs que são utilizadas pela API
4. `004_create_functions.sql` – cria as funções relacionadas com a lógica de incremento e decremento da quantidade de funcionarios de cada setor 
5. `005_create_tables.sql` – cria as triggers para possibilitar a execução das funções criadas quando houver determinadas ações
> Banco recomendado: PostgreSQL