using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using Dapper;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.Services
{
    public static class ServicesRepository
    {
        /*Recebe nome do setor e retorna seu id se existir e 0 se nao existir*/
        public static async Task<int> VerificaSetor(string nome)
        {
            using (var connection = DbConennectionDapper.GetStringConnection())
            {
                string query = "SELECT id FROM setor WHERE nome=@Nome";
                var resultQuery =await connection.QuerySingleAsync<int>(query, new { Nome = nome });
                return resultQuery;
            }
        }
    }
}
