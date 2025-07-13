using API_ARMAZENA_FUNCIONARIOS.Model.EnumModel;
using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using API_ARMAZENA_FUNCIONARIOS.Services.ServiceCripto;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServicesUsersLogin
{
    public class UsersLoginService : IUsersLoginService
    {
        private readonly IRepositoryUsersLogin repositoryUsersLogin;
        public UsersLoginService(IRepositoryUsersLogin usersLogin)
        {
            this.repositoryUsersLogin = usersLogin ?? throw new ArgumentNullException(nameof(usersLogin)); 
        }

        public async Task<ModelUsers?> Login(UserLoginRequest userEntrada)
        {
            if (userEntrada == null)
            {
                return null;
            }


            // deve retornar "null" para user caso não exista ou Objeto com senha,salt,username
            ModelUsers? usuarioBanco = await repositoryUsersLogin.UsernameExiste(userEntrada.username);
            if (usuarioBanco == null) {
                // usuário não existe
                return null;
            }


            // Codificar user.senha 
            bool resultadoSenha = Cripto.VerificarSenhaCripto(userEntrada.senha,usuarioBanco.senha_hash,usuarioBanco.salt);
            if (resultadoSenha)
            {
                return usuarioBanco;
            }
            return null;
        }


        public async Task<bool> Register(UserRegisterRequest userEntrada)
        {

            if (userEntrada == null)
            {
                return false;
            }


            // deve retornar "null" para user caso não exista ou Objeto com senha,salt,username
            ModelUsers? usuarioBanco = await repositoryUsersLogin.UsernameExiste(userEntrada.username);
            if (usuarioBanco != null)
            {
                // usuário existe com esse username
                return false;
            }

            //hash da senha e pegando o salt
            var senhaSalt = Cripto.GerarCriptoSenha(userEntrada.senha);


            // criando no tipo model
            ModelUsers usuario = new ModelUsers(userEntrada.username,senhaSalt.novoHash,senhaSalt.salt,EnumStatus.ativo,userEntrada.role);
            
            
            // criar usuário no banco, seria INSERT
            if (await repositoryUsersLogin.CriarUser(usuario)) 
            {
                return true;
            }

            return false ;
        }

    }
}
