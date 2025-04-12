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
        public async Task<IActionResult> Get(string nomeSetor) {
            List<FuncionarioResponse> clientes= await clientesLojaFisica.ListarFuncionario(nomeSetor);
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
        public async Task<IActionResult> GetUnit(string cpf)
        {
            FuncionarioResponse funcionario = await clientesLojaFisica.PegarFuncionario(cpf);
            if (funcionario != null)
            {
                return Ok(funcionario); //retorna como json
            }
            else
            {
                return BadRequest(new { mensagem = "CPF INVALIDO" });
            }
        } 

        [HttpPost]
        [Route("/inserirFuncionario")]
        public async Task<IActionResult> Post([FromBody]FuncionarioRequest funcionario)
        {   

            if(await clientesLojaFisica.SalvarFuncionario(funcionario))
            {
                return Ok("Salvo com sucesso: Nome = "+ funcionario.nome);
            }
            return BadRequest(new { mensagem="Valor inadequados para serem salvos" });
        }
    }
}
