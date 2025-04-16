using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using Dapper;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.Services
{
    public static class ServicesRepository
    {
        /// <summary>
        /// Recebe nome do setor e retorna seu id se existir e 0 se nao existir
        /// </summary>
        /// <param name="verificaSetor">Verifica Setor</param>
        /// <returns>ID diferente de 0, caso 0 não existe</returns>
        public static async Task<int> VerificaSetor(string nome)
        {
            using (var connection = DbConennectionDapper.GetStringConnection())
            {
                string query = "SELECT id FROM setores WHERE nome=@Nome";
                var resultQuery =await connection.QuerySingleAsync<int>(query, new { Nome = nome });
                return resultQuery;
            }
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
    }
}
