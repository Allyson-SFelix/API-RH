using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionContext;
using API_ARMAZENA_FUNCIONARIOS.Infraestrutura.ConnectionDapper;
using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.Repository.Services;
using Dapper;

namespace API_ARMAZENA_FUNCIONARIOS.Repository
{
    public class RepositoryUsers : IRepositoryUsersLogin
    {
        private readonly DbConnectionContext _connection;

        public RepositoryUsers(DbConnectionContext connection)
        {
            _connection = connection;
        }


        public async Task<ModelUsers?> UsernameExiste(string username)
        {
            using (var conn = DbConennectionDapper.GetStringConnection())
            {

                string query = "SELECT id,username,senha_hash,salt,roles,status FROM users " +
                "WHERE username=@user;";
                try
                {
                    return await conn.QueryFirstOrDefaultAsync<ModelUsers>(query, new { user = username });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

        }

        public async Task<bool> CriarUser(ModelUsers user)
        {
            if (user == null) return false;

            try
            {

                var res =await _connection.Users.AddAsync(user);

                await RepositorioHelper.CommitChanges(this._connection);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em SalvarCliente: {ex.Message}");
                return false;
            }
        }
    }
}
