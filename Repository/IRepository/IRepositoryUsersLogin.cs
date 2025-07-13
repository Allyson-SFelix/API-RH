using API_ARMAZENA_FUNCIONARIOS.Model.Tables;

namespace API_ARMAZENA_FUNCIONARIOS.Repository.IRepository
{
    public interface IRepositoryUsersLogin
    {

        public Task<ModelUsers?> UsernameExiste(string username);

        public Task<bool> CriarUser(ModelUsers user);
    }
}
