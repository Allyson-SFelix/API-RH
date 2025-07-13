using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServicesUsersLogin
{
    public interface IUsersLoginService
    {

        public Task<bool> Login(UserLoginRequest user);


        public Task<bool> Register(UserRegisterRequest userEntrada);
    }
}
