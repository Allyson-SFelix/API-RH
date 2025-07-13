using API_ARMAZENA_FUNCIONARIOS.Model.Tables;
using API_ARMAZENA_FUNCIONARIOS.Services.ServicesUsersLogin;
using API_ARMAZENA_FUNCIONARIOS.Services.TokenAuth;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_ARMAZENA_FUNCIONARIOS.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersLoginService usersLoginService ;
        public UsersController(IUsersLoginService usersLogin)
        {
            this.usersLoginService = usersLogin ?? throw new ArgumentNullException(nameof(usersLogin)); ;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
         public async Task<IActionResult> Login([FromBody]UserLoginRequest usersLogin)
        {
            ModelUsers? resultado = await usersLoginService.Login(usersLogin);
            if (resultado!=null)
            {
                var token = ConfigurationToken.GenerationToken(resultado);
                return Ok(new { Authorization = token });
            }

            return BadRequest(new { Authorization = "false" });
        }

        [Authorize(Roles = "admin,gerente")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest usersLogin)
        {
            bool resultado = await usersLoginService.Register(usersLogin);
            if (resultado)
            {
                return Ok(new { Authorization = "true" });
            }

            return BadRequest(new { Authorization = "false" });
        }
    }
}
