using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using Dapper;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.Services
{
    public static class ServicesRepository
    {
        /// <summary>
        /// Recebe nome do setor e retorna seu id se existir e 0 se nao existir
        /// </summary>
        /// <param name="verificaSetor">Verifica Setor</param>
        /// <returns>ID diferente de 0, caso 0 não existe </returns>
        public static async Task<int> VerificaSetor(string nome)
        {
            using (var connection = DbConennectionDapper.GetStringConnection())
            {
                string query = "SELECT id FROM setores WHERE nome=@Nome";
                var resultQuery =await connection.QuerySingleOrDefaultAsync<int>(query, new { Nome = nome});
                return resultQuery;
            }
        }


        /// <summary>
        /// Recebe nome do setor e retorna se existe ou nao o setor buscado pelo nome
        /// </summary>
        /// <param name="verificaSetorExiste">Verifica Setor Existe</param>
        /// <returns> true existe setor ativo, false nao existe esse setor ou nao esta ativo com esse nome</returns>
        public static async Task<bool> VerificarSetorExiste(string nome)
        {
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                string sql = "SELECT id FROM setores WHERE nome=@nome AND status=@status::enum_status";
                var result = await conn.QueryFirstOrDefaultAsync<int>(sql, new { nome = nome, status=EnumStatus.ativo.ToString() });
                if (result == 0)
                {
                    return false;
                }
                return true;
            };
        }

        /// <summary>
        /// Recebe o contexto do banco de dados de determinado Repository e realiza o commit das mudanças ocorridas no banco
        /// </summary>
        /// <param name="comitChanges">Commit Changes</param>
        /// <returns>Task</returns>
        public static async Task CommitChanges(DbConnectionContext _connection)
        {
              await _connection.SaveChangesAsync();
        }



        /// <summary>
        /// Recebe o cpf e verifica se é único entre os funcionarios existente na tabela de funcionarios
        /// </summary>
        /// <param name="CpfUniq">Cpf Unico</param>
        /// <returns> false para CPF Não único ou string vazia, retorna true se for CPF unico</returns>
        public static async Task<bool> CpfUniq(string cpf)
        {
            if (cpf == "")
            {
                return false;
            }

            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT id FROM funcionarios WHERE cpf=@cpfEntrada";
                    int result = await conn.QueryFirstOrDefaultAsync<int>(query, new { cpfEntrada= cpf });
                    if(result != 0)
                    {
                        return false;
                    }
                    return true;
                }catch(Exception e)
                {
                    Console.WriteLine("Exception: "+e.Message);
                    return false;
                }
            }
        }


        /// <summary>
        /// Recebe o id do setor e retorna a quantidade
        /// </summary>
        /// <param name="QtdFuncionariosSetor">Quantidade de Funcionarios no Setor</param>
        /// <returns> quantidade ou retorna -1 para determinar um erro</returns>
        public static async Task<int> QtdFuncionariosSetor(int id){
            using (var conn = DbConennectionDapper.GetStringConnection())
            {
                try
                {
                    string query = "SELECT qtd_funcionarios FROM setores WHERE id=@BuscaID";

                    return await conn.QueryFirstOrDefault(query, new { BuscaID = id });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    return -1;
                }
            }
        }
    }
}
