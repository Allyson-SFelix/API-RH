using API_ARMAZENA_FUNCIONARIOS.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionario clientesLojaFisica;

        public FuncionariosController(IFuncionario clientesLojaFisica)
        {
            this.clientesLojaFisica = clientesLojaFisica ?? throw new ArgumentNullException(nameof(clientesLojaFisica));
        }

        [HttpGet]
        [Route("/listaFuncionarios")]
        public async Task<IActionResult> Get() {
            List<FuncionarioResponse> clientes= await clientesLojaFisica.ListarFuncionario();
            if (clientes != null)
            {
                return Ok(clientes); //return como json
            }
            else
            {
                return BadRequest(new { Messagem = "Lista vazia" });
            }
        }

        [HttpGet]
        [Route("/unitFuncionario")]
        public async Task<IActionResult> GetUnit(int id)
        {
            FuncionarioResponse cliente = await clientesLojaFisica.PegarFuncionario(id);
            if (cliente != null)
            {
                return Ok(cliente); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "ID INVALIDO" });
            }
        } 

        [HttpPost]
        [Route("/inserirFuncionario")]
        public async Task<IActionResult> Post([FromBody]FuncionarioRequest funcionario)
        {
            if(await clientesLojaFisica.SalvarFuncionario(funcionario))
            {
                return Ok("Salvo com sucesso: Nome ="+ funcionario.nome+"\nIdade ="+ funcionario.idade);
            }
            return BadRequest(new { mensagem="Valor inadequados para serem salvos" });
        }
    }
}
