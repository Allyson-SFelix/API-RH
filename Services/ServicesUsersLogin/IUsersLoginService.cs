using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServicesUsersLogin
{
    public interface IUsersLoginService
    {

        public Task<ModelUsers?> Login(UserLoginRequest user);


        public Task<bool> Register(UserRegisterRequest userEntrada);
    }
}
