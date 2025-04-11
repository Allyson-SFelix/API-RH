using System.Data;
using Dapper;
using Npgsql;

namespace API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper
{
    public static class DbConennectionDapper 
    {
        public static string _connectionString;

        public static void SetConnection(string stringConnect)
        {
            _connectionString = stringConnect;
        }

        //retorna a conexão para ser aberta e executada e comitada(quando necessario)
        public static IDbConnection GetStringConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
